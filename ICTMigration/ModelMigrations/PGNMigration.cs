using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class PGNMigration
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        public PGNMigration()
        {
            ictv2Model = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
        }

        private async Task MigrateBindedAccounts()
        {
            var requestV2Accounts = ictv2Model.PGNRequestAccounts.ToList();
            foreach (var account in requestV2Accounts)
            {
                var requestV3 = await unitOfWork.PGNRequestsRepo.FindAsync(x => x.Id == account.fldRequestId);
                if (requestV3 == null) continue;

            }
        }
        public async Task MigratePGNRequests()
        {
            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('PGNRequests', RESEED, 99);");
            var requestV2 = ictv2Model.PGNRequests.ToList();
            var lastId = requestV2.OrderBy(x => x.Id).LastOrDefault().RequestId;

            for (var i = 100; i <= lastId; i++)
            {
                var currentV2Request = requestV2.FirstOrDefault(x => x.RequestId == i);
                if (currentV2Request == null)
                {
                    var pgnRequest = new PGNRequests();
                    unitOfWork.PGNRequestsRepo.Insert(pgnRequest);
                }
                else
                {
                    var currentUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == currentV2Request.RequestBy);
                    long? empId = null;
                    if (!string.IsNullOrEmpty(currentV2Request?.SourceEmployee))
                    {
                        string s = currentV2Request?.SourceEmployee;
                        empId = HRMISEmployees.GetEmployees().FirstOrDefault(x => x.Username == s)?.Id;
                    }
                    var pgnRequest = new PGNRequests()
                    {
                        DateCreated = currentV2Request.DateRequested,
                        RequestDate = currentV2Request.DateRequested,
                        CommunicationType = currentV2Request.CommType == 100 ? CommunicationType.PGN : CommunicationType.TA,
                        SignatoryId = empId,
                        Subject = currentV2Request.Subject,
                        CreatedById = currentUser.Id
                    };
                    unitOfWork.PGNRequestsRepo.Insert(pgnRequest);
                }
            }
            await unitOfWork.SaveChangesAsync();
            unitOfWork.PGNRequestsRepo.DeleteRange(x => x.DateCreated == null);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task MigratePGNAccounts()
        {
            var empV2 = ictv2Model.PGNAccounts.ToList();
            var lastId = empV2.OrderBy(x => x.Id).LastOrDefault().Id;

            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('PGNAccounts', RESEED, 0);");
            for (var i = 0; i <= lastId; i++)
            {
                var currentV2Emp = empV2.FirstOrDefault(x => x.Id == i);
                if(currentV2Emp == null)
                {
                    var pgnAccount = new PGNAccounts();
                    unitOfWork.PGNAccountsRepo.Insert(pgnAccount);
                }
                else
                {
                    if(currentV2Emp.NonEmployee == true)
                    {
                        var nonEmpExist = await unitOfWork.PGNNonEmployeeRepo.FindAsync(x => x.Id == currentV2Emp.EmpId);
                        if (nonEmpExist == null) continue;
                    }
                    var pgnAccount = new PGNAccounts
                    {
                        HRMISEmpId = currentV2Emp.NonEmployee == false ? currentV2Emp.EmpId : null,
                        Username = currentV2Emp.UserName,
                        UserType = GetUserType(currentV2Emp.UserType),
                        Status = currentV2Emp.Status == "Enabled" ? PGNStatus.Enabled : PGNStatus.Disabled,
                        IPAddress = currentV2Emp.UserType.Contains(".") ? currentV2Emp.UserType : null,
                        SignInCount = currentV2Emp.SigninCount,
                        TrafficSpeed = GetTrafficSpeed(currentV2Emp.TrafficSpeed),
                        Designation = GetDesignation(currentV2Emp.Designation),
                        Remarks = currentV2Emp.Remarks,
                        Password = currentV2Emp.Password,
                        PGNNonEmployeeId = currentV2Emp.NonEmployee == true ? currentV2Emp.EmpId : null
                    };
                    unitOfWork.PGNAccountsRepo.Insert(pgnAccount);

                    var macAddresses = currentV2Emp.PGNMacAdderesses.ToList();
                    foreach (var macAddress in macAddresses)
                    {
                        var newPGNMac = new PGNMacAddresses
                        {
                            PGNAccounts = pgnAccount,
                            Connection = macAddress.DeviceName.Contains("LAN") ? PGNDeviceConnection.LAN : PGNDeviceConnection.WIFI,
                            MacAddress = macAddress.DeviceMac,
                            Device = GetDevice(macAddress.DeviceName)
                        };
                        unitOfWork.PGNMacAddressesRepo.Insert(newPGNMac);
                    }
                }
            }
            await unitOfWork.SaveChangesAsync();
            unitOfWork.PGNAccountsRepo.DeleteRange(x => x.Username == null);
            await unitOfWork.SaveChangesAsync();
        }

        private PGNDevices GetDevice(string device)
        {
            if (device.Contains("PC")) return PGNDevices.PC;
            if(device.Contains("Laptop") || device.Contains("LAPTOP")) return PGNDevices.Laptop;
            return PGNDevices.Phone;
        }
        private PGNDesignations? GetDesignation(string designation)
        {
            if(designation == "ADMIN") return PGNDesignations.Admin;
            if (designation == "Technical") return PGNDesignations.Technical;
            if (designation == "ADH") return PGNDesignations.ADH;
            if (designation == "Elective") return PGNDesignations.Elective;
            return null;
        }
        private PGNTrafficSpeed? GetTrafficSpeed(string speed)
        {
            if(speed == "8Mbps") return PGNTrafficSpeed._8Mbps;
            if(speed == "High Guarantee for guests") return PGNTrafficSpeed.HighGuaranteeUser;
            if (speed == "No policy") return PGNTrafficSpeed.NoPolicy;
            if (speed == "3Mbps") return PGNTrafficSpeed._3Mbps;
            if (speed == "10Mbps") return PGNTrafficSpeed._10Mbps;
            if (speed == "High Guarantee User") return PGNTrafficSpeed.HighGuaranteeUser;
            if (speed == "5Mbps") return PGNTrafficSpeed._5Mbps;
            if (speed == "1Mbps") return PGNTrafficSpeed._1Mbps;
            if (speed == "Moderate Guarantee User") return PGNTrafficSpeed.ModerateGuaranteeUser;
            if (speed == "for PDRRMO") return PGNTrafficSpeed.HighGuaranteeUser;
            return null;
        }

        private PGNUserType GetUserType(string usertype) 
        {
            if (usertype == "User") return PGNUserType.User;
            if (usertype == "Administrator") return PGNUserType.Admin;
            return PGNUserType.Clientless;
        }
        public async Task MigrateNonEmployee()
        {
            var nonEmpV2 = ictv2Model.PGNNonEmployees.ToList();
            var lastId = nonEmpV2.OrderBy(x => x.Id)?.LastOrDefault()?.Id;

            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('PGNNonEmployees', RESEED, 0);");
            for (var i = 0; i <= lastId; i++)
            {
                var currentV2Emp = nonEmpV2.FirstOrDefault(x => x.Id == i);
                if (currentV2Emp == null) 
                {
                    var newEmp = new Models.Entities.PGNNonEmployee();
                    unitOfWork.PGNNonEmployeeRepo.Insert(newEmp);
                }
                else
                {
                    var fullName = currentV2Emp.Fullname == null ? 
                        currentV2Emp.Firstname + " " + currentV2Emp.Middlename + " " + currentV2Emp.Lastname + " " + currentV2Emp.NameExt
                        : currentV2Emp.Fullname;

                    var newEmp = new Models.Entities.PGNNonEmployee
                    {
                        Username = currentV2Emp.Username,
                        FullName = fullName,
                        Position = currentV2Emp.Designation
                    };
                    unitOfWork.PGNNonEmployeeRepo.Insert(newEmp);
                }
            }
            await unitOfWork.SaveChangesAsync();
            unitOfWork.PGNNonEmployeeRepo.DeleteRange(x => x.Username == null);
            await unitOfWork.SaveChangesAsync();    
        }
    }
}
