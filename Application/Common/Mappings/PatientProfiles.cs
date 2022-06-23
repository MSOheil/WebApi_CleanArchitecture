using Application.Common.Models;
using Application.Patients.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class PatientProfiles :Profile
    {
        public PatientProfiles()
        {

            CreateMap<ReadPatientDto, Patient>().ReverseMap();
            CreateMap< ReadPatientWithIdDto,Patient >().ReverseMap().ForMember(d => d.Visits, o => o.MapFrom(s => s.Visits));
        }
    }
}
