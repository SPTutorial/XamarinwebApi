using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XamarinwebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Addresss { get; set; }
        public string PhoneNumber { get; set; }
    }
}