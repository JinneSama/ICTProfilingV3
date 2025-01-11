using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class LookUpMigration
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        public LookUpMigration()
        {
            ictv2Model = new ICTv2Entities();   
            unitOfWork = new UnitOfWork();
        }

        public async Task MigrateActionDropdowns()
        {
            var dropdowns = ictv2Model.Dropdowns.ToList();
            var maxId = dropdowns.OrderBy(o => o.Id).LastOrDefault().Id;

            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('ActionsDropdowns', RESEED, 998);");
            for (int i = 999; i <= maxId; i++)
            {
                var dropdown = dropdowns.FirstOrDefault(x => x.Id == i);

                if(dropdown == null)
                {
                    var newDropdown = new ActionsDropdowns();
                    unitOfWork.ActionsDropdownsRepo.Insert(newDropdown);
                }
                else
                {
                    var newDropdown = new ActionsDropdowns
                    {
                        Value = dropdown.Value,
                        ParentId = dropdown.ParentId,
                        Order = dropdown.Order,
                        ActionCategory = GetActionCategory(dropdown.Category)
                    };
                    unitOfWork.ActionsDropdownsRepo.Insert(newDropdown);
                }
            }
            await unitOfWork.SaveChangesAsync();

            unitOfWork.ActionsDropdownsRepo.DeleteRange(x => x.ActionCategory == null);
            await unitOfWork.SaveChangesAsync();
        }

        private ActionCategory? GetActionCategory(string category)
        {
            if (category == "Programs") return ActionCategory.Programs;
            if (category == "Projects") return ActionCategory.MainAct;
            if (category == "Activity") return ActionCategory.Activity;
            if (category == "SubActivity" || category == "Sub Activity" || category == null) return ActionCategory.SubActivity;
            return null;
        }
        public async Task MigrateActionList()
        {
            var actionlist = ictv2Model.ActionLists.ToList();

            foreach (var action in actionlist) {
                var newlist = new ActionTaken
                {
                    DateAdded = action.DateCreated,
                    Action = action.Action
                };
                unitOfWork.ActionTakenRepo.Insert(newlist);
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigrateEquipments()
        {
            var suppliers = ictv2Model.Suppliers.ToList();
            foreach (var supplier in suppliers)
            {
                var newSupplier = new Models.Entities.Supplier
                {
                    SupplierName = supplier.SupplierName,
                    Address = supplier.Address,
                    TelNumber = supplier.PhoneNumber,
                    ContactPerson = supplier.ContactNumber,
                    FaxNumber = supplier.FaxNumber,
                    Status = supplier.Status,
                    IsDeleted = supplier.IsDeleted,
                    OldPK = supplier.Id
                };

                unitOfWork.SupplierRepo.Insert(newSupplier);
            }
            await unitOfWork.SaveChangesAsync();

            var ticketEquipments = ictv2Model.TicketEquipments.ToList();
            foreach (var ticketEquipment in ticketEquipments)
            {
                var equipment = new Equipment
                {
                    EquipmentName = ticketEquipment.Equipment,
                    OldPK = ticketEquipment.Id
                };
                unitOfWork.EquipmentRepo.Insert(equipment);
            }
            await unitOfWork.SaveChangesAsync();

            var ticketICTSpecs = ictv2Model.TicketICTSpecs
                .Include(x => x.TicketICTSpecsDetails)
                .Include(x => x.TicketEquipment)
                .Include(x => x.TicketEquipmentBrands)
                .Include(x => x.TicketEquipmentBrands.Select(s => s.TicketEquipmentModels))
                .ToList();
            foreach(var ticketICTSpec in ticketICTSpecs)
            {
                var equipment = await unitOfWork.EquipmentRepo.FindAsync(x => x.EquipmentName == ticketICTSpec.TicketEquipment.Equipment);

                var ticketEquipmentSpecsDetails = ticketICTSpec.TicketICTSpecsDetails.ToList();

                var EquipmentSpecs = new EquipmentSpecs
                {
                    Description = ticketICTSpec.Description,
                    Remarks = ticketICTSpec.Remarks,
                    Equipment = equipment,
                    OldPK = ticketICTSpec.Id
                };
                unitOfWork.EquipmentSpecsRepo.Insert(EquipmentSpecs);

                foreach (var item in ticketEquipmentSpecsDetails)
                {
                    var equipmentSpecsDetail = new EquipmentSpecsDetails
                    {
                        ItemNo = (int)item.ItemNo,
                        DetailDescription = item.EquipmentDescrip,
                        DetailSpecs = item.EquipmentSpecs,
                        EquipmentSpecs = EquipmentSpecs
                    };
                    unitOfWork.EquipmentSpecsDetailsRepo.Insert(equipmentSpecsDetail);
                }

                var tsBasis = ticketICTSpec.TechSpecsBasis.ToList();
                foreach(var basis in tsBasis)
                {
                    var newBasis = new TechSpecsBasis
                    {
                        EquipmentSpecs = EquipmentSpecs,
                        PriceRange = (double)basis.PriceRange,
                        PriceDate = basis.PriceDate,
                        URLBasis = basis.Basis,
                        Remarks = basis.Remarks,
                        Available = basis.Available
                    };
                    unitOfWork.TechSpecsBasisRepo.Insert(newBasis);
                }

                var ticketBrands = ticketICTSpec.TicketEquipmentBrands.ToList();
                foreach (var ticketBrand in ticketBrands)
                {
                    var brand = new Brand
                    {
                        BrandName = ticketBrand.Brand,
                        EquipmentSpecs = EquipmentSpecs,
                        OldPK = ticketBrand.Id
                    };
                    unitOfWork.BrandRepo.Insert(brand);
                    var ticketModels = ticketBrand.TicketEquipmentModels.ToList();
                    foreach(var ticketModel in ticketModels)
                    {
                        var model = new Model
                        {
                            ModelName = ticketModel.Model,
                            Brand = brand,
                            OldPK = ticketModel.Id
                        };
                        unitOfWork.ModelRepo.Insert(model);
                    }
                }
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigrateStandardPR()
        {
            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('StandardPRSpecs', RESEED, 0);");
            var standardPRSpecs = ictv2Model.PREquipments.ToList();

            var total = standardPRSpecs.OrderBy(x => x.Id).LastOrDefault().Id;
            for(var i = 1; i <= total; i++)
            {
                var prSpecResult = standardPRSpecs.FirstOrDefault(x => x.Id == i);
                if(prSpecResult == null)
                {
                    var spec = new StandardPRSpecs();
                    unitOfWork.StandardPRSpecsRepo.Insert(spec);
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    var eSpecs = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.OldPK == prSpecResult.EquipmentType);
                    var spec = new StandardPRSpecs()
                    {
                        ItemNo = prSpecResult.ItemNo ?? 0,
                        Quarter = GetQuarter(prSpecResult.Quarter),
                        Description = prSpecResult.Description,
                        Purpose = prSpecResult.Purpose,
                        Unit = Unit.unit,
                        UnitCost = (long?)prSpecResult.UnitCost,
                        EquipmentSpecsId = eSpecs.Id
                    };
                    unitOfWork.StandardPRSpecsRepo.Insert(spec);

                    var specsDetails = prSpecResult.StandardPRSpecs;
                    foreach(var specDetails in specsDetails)
                    {
                        var specDetail = new StandardPRSpecsDetails()
                        {
                            ItemNo = specDetails.ItemNo ?? 0,
                            Specs = specDetails.Specs,
                            Description = specDetails.Description,
                            StandardPRSpecs = spec
                        };
                        unitOfWork.StandardPRSpecsDetailsRepo.Insert(specDetail);
                    }
                    await unitOfWork.SaveChangesAsync();
                }
            }
            unitOfWork.StandardPRSpecsRepo.DeleteRange(x => x.EquipmentSpecs == null);
            await unitOfWork.SaveChangesAsync();
        }
        private Unit GetUnit(string unit)
        {
            Enum.TryParse(unit, out Unit enumUnit);
            return enumUnit;
        }

        private PRQuarter GetQuarter(int? Quarter)
        {
            PRQuarter _quarter = PRQuarter.Fourth;
            switch (Quarter)
            {
                case 1: _quarter = PRQuarter.First; break;
                case 2: _quarter = PRQuarter.Second; break;
                case 3: _quarter = PRQuarter.Third; break;
                case 5: _quarter = PRQuarter.Fourth; break;
            }
            return _quarter;
        }
    }
}
