﻿namespace ClassTrack.BusinessLogicLayer.Dtos.Group
{
    public class AddGroupDto
    {
        [Required]
        public string Name { get; set; }

        public string? TeacherId { get; set; }  
    }
}
