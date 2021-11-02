using System;
namespace IvarsSykkelsjappe.Data.Models
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Extension { get; set; }

        public int BikeId { get; set; }

        public virtual Bike Bike { get; set; }
    }
}
