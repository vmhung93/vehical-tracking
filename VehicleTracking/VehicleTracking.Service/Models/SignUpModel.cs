﻿using System.ComponentModel.DataAnnotations;

namespace VehicleTracking.Service.Models
{
    public class SignUpModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
