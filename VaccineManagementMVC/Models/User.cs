﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VaccineManagementMVC.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        [Range(18, 100, ErrorMessage = "You are not eligible for Vaccination")]
        public int? Age { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string PhoneNo { get; set; }
        public string AadhaarNo { get; set; }
        // public virtual  ICollection<Slot> Slots { get; set; }
    }
}