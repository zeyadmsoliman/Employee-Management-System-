﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }
        public int Gender{ get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId{ get; set; }
        public Department Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastWorkingDate { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int UserId { get; set; }
        public UserData user { get; set; }
        

    }
}
