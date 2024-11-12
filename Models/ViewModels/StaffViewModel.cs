using Models.Entities;
using System.Drawing;

namespace Models.ViewModels
{
    public class StaffViewModel
    {
        public Users Users { get; set; }
        public Image Image => Image.FromFile("C:\\Users\\Scott\\Downloads\\employee.jpg");
    }
}
