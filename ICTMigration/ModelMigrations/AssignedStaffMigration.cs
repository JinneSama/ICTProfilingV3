using Models.Entities;
using Models.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class AssignedStaffMigration
    {
        private readonly IUnitOfWork unitOfWork;
        public AssignedStaffMigration()
        {
            unitOfWork = new UnitOfWork();
        }
        public async Task GetAssignedUsersDeliveries()
        {
            Users exemptedUser = await unitOfWork.UsersRepo.FindAsync(x => x.Id == "5ce43192-2a51-4454-b9ae-38b2c1154c1c");
            var deliveries = unitOfWork.DeliveriesRepo.GetAll(x => x.Actions, x => x.Actions.Select(s => s.RoutedUsers));

            foreach (var item in deliveries)
            {
                var assignedUser = item.Actions.ToList();
                var sentUsers = assignedUser.Where(x => x.IsSend == true && x.RoutedUsers.Any() && !x.RoutedUsers.Contains(exemptedUser));
                var user = sentUsers?.OrderBy(x => x.DateCreated)?.FirstOrDefault()?.RoutedUsers?.FirstOrDefault();

                if (assignedUser.Count <= 0) continue;
                if (user == null)
                {
                    var userlist = assignedUser?.OrderBy(x => x.DateCreated)?.ToList();
                    if (userlist.Count < 2) user = null;
                    else user = userlist[1].CreatedBy;

                    if (user == null)
                    {
                        user = assignedUser?.OrderBy(x => x.DateCreated)?.ToList()[0].CreatedBy;
                    }
                }
                var uow = new UnitOfWork();
                var staff = uow.ITStaffRepo.GetAll(x => x.Users).ToList().FirstOrDefault(w => w.Users.Id == user.Id);

                var ticket = await uow.TicketRequestRepo.FindAsync(x => x.Id == item.Id);
                if(staff == null) continue;
                ticket.StaffId = staff.Id;
                await uow.SaveChangesAsync();
            }
        }
        public async Task GetAssignedUsersRepair()
        {
            Users exemptedUser = await unitOfWork.UsersRepo.FindAsync(x => x.Id == "5ce43192-2a51-4454-b9ae-38b2c1154c1c");
            var repair = unitOfWork.RepairsRepo.GetAll(x => x.Actions, x => x.Actions.Select(s => s.RoutedUsers));

            foreach (var item in repair)
            {
                var assignedUser = item.Actions.ToList();
                var sentUsers = assignedUser.Where(x => x.IsSend == true && x.RoutedUsers.Any() && !x.RoutedUsers.Contains(exemptedUser));
                var user = sentUsers?.OrderBy(x => x.DateCreated)?.FirstOrDefault()?.RoutedUsers?.FirstOrDefault();

                if (assignedUser.Count <= 0) continue;
                if (user == null)
                {
                    var userlist = assignedUser?.OrderBy(x => x.DateCreated)?.ToList();
                    if (userlist.Count < 2) user = null;
                    else user = userlist[1].CreatedBy;

                    if (user == null)
                    {
                        user = assignedUser?.OrderBy(x => x.DateCreated)?.ToList()[0].CreatedBy;
                    }
                }
                var uow = new UnitOfWork();
                var staff = uow.ITStaffRepo.GetAll(x => x.Users).ToList().FirstOrDefault(w => w.Users.Id == user.Id);

                var ticket = await uow.TicketRequestRepo.FindAsync(x => x.Id == item.Id);
                if (staff == null) continue;
                ticket.StaffId = staff.Id;
                await uow.SaveChangesAsync();
            }
        }
        public async Task GetAssignedUsersTS()
        {
            Users exemptedUser = await unitOfWork.UsersRepo.FindAsync(x => x.Id == "5ce43192-2a51-4454-b9ae-38b2c1154c1c");
            var ts = unitOfWork.TechSpecsRepo.GetAll(x => x.Actions, x => x.Actions.Select(s => s.RoutedUsers));

            foreach (var item in ts)
            {
                var assignedUser = item.Actions.ToList();
                var sentUsers = assignedUser.Where(x => x.IsSend == true && x.RoutedUsers.Any() && !x.RoutedUsers.Contains(exemptedUser));
                var user = sentUsers?.OrderBy(x => x.DateCreated)?.FirstOrDefault()?.RoutedUsers?.FirstOrDefault();

                if (assignedUser.Count <= 0) continue;
                if (user == null)
                {
                    var userlist = assignedUser?.OrderBy(x => x.DateCreated)?.ToList();
                    if (userlist.Count < 2) user = null;
                    else user = userlist[1].CreatedBy;

                    if (user == null)
                    {
                        user = assignedUser?.OrderBy(x => x.DateCreated)?.ToList()[0].CreatedBy;
                    }
                }
                var uow = new UnitOfWork();
                var staff = uow.ITStaffRepo.GetAll(x => x.Users).ToList().FirstOrDefault(w => w.Users.Id == user.Id);

                var ticket = await uow.TicketRequestRepo.FindAsync(x => x.Id == item.Id);
                if(staff == null) continue;
                ticket.StaffId = staff.Id;
                await uow.SaveChangesAsync();
            }
        }
    }
}
