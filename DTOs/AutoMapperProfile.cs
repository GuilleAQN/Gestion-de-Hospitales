using AutoMapper;
using Primer_Parcial.DTOs.CategoriasCita;
using Primer_Parcial.DTOs.Cita;
using Primer_Parcial.DTOs.Departamento;
using Primer_Parcial.DTOs.Diagnostico;
using Primer_Parcial.DTOs.Doctor;
using Primer_Parcial.DTOs.Enfermera;
using Primer_Parcial.DTOs.Especialidad;
using Primer_Parcial.DTOs.Estado;
using Primer_Parcial.DTOs.Habitacion;
using Primer_Parcial.DTOs.Paciente;
using Primer_Parcial.DTOs.Tratamiento;

namespace Primer_Parcial.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoriaCitaGetDTO, Models.CategoriasCita>().ReverseMap();
            CreateMap<CategoriaCitaInsertDTO, Models.CategoriasCita>().ReverseMap();
            CreateMap<CategoriaCitaUpdateDTO, Models.CategoriasCita>().ReverseMap();

            CreateMap<CitaGetDTO, Models.Cita>().ReverseMap();
            CreateMap<CitaInsertDTO, Models.Cita>().ReverseMap();
            CreateMap<CitaUpdateDTO, Models.Cita>().ReverseMap();

            CreateMap<DepartamentoGetDTO, Models.Departamento>().ReverseMap();
            CreateMap<DepartamentoInsertDTO, Models.Departamento>().ReverseMap();
            CreateMap<DepartamentoUpdateDTO, Models.Departamento>().ReverseMap();

            CreateMap<DiagnosticoGetDTO, Models.Diagnostico>().ReverseMap();
            CreateMap<DiagnosticoInsertDTO, Models.Diagnostico>().ReverseMap();
            CreateMap<DiagnosticoUpdateDTO, Models.Diagnostico>().ReverseMap();

            CreateMap<DoctorGetDTO, Models.Doctore>().ReverseMap();
            CreateMap<DoctorInsertDTO, Models.Doctore>().ReverseMap();
            CreateMap<DoctorUpdateDTO, Models.Doctore>().ReverseMap();

            CreateMap<EnfermeraGetDTO, Models.Enfermera>().ReverseMap();
            CreateMap<EnfermeraInsertDTO, Models.Enfermera>().ReverseMap();
            CreateMap<EnfermeraUpdateDTO, Models.Enfermera>().ReverseMap();

            CreateMap<EspecialidadGetDTO, Models.Especialidade>().ReverseMap();
            CreateMap<EspecialidadInsertDTO, Models.Especialidade>().ReverseMap();
            CreateMap<EspecialidadUpdateDTO, Models.Especialidade>().ReverseMap();

            CreateMap<EstadoGetDTO, Models.Estado>().ReverseMap();
            CreateMap<EstadoInsertDTO, Models.Estado>().ReverseMap();
            CreateMap<EstadoUpdateDTO, Models.Estado>().ReverseMap();

            CreateMap<HabitacionGetDTO, Models.Habitacione>().ReverseMap();
            CreateMap<HabitacionInsertDTO, Models.Habitacione>().ReverseMap();
            CreateMap<HabitacionUpdateDTO, Models.Habitacione>().ReverseMap();

            CreateMap<PacienteGetDTO, Models.Paciente>().ReverseMap();
            CreateMap<PacienteInsertDTO, Models.Paciente>().ReverseMap();
            CreateMap<PacienteUpdateDTO, Models.Paciente>().ReverseMap();

            CreateMap<TratamientoGetDTO, Models.Tratamiento>().ReverseMap();
            CreateMap<TratamientoInsertDTO, Models.Tratamiento>().ReverseMap();
            CreateMap<TratamientoUpdateDTO, Models.Tratamiento>().ReverseMap();
        }
    }
}
