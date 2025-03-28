using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    public class DietaryServiceRequestDto
    {
        public List<InpatientDietSfxmdyEntity> EditmxList { get; set; }
        public List<string> DelIds { get; set; }
    }
}
