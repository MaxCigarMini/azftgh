using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Model;
using ZAMETKI_FINAL.Services;
using ZAMETKI_FINAL.Dto_Vm;

namespace ZAMETKI_FINAL.Mappings
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<NoteDto, Note>()
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.DateTimeCreate, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<NoteUpdateDto, Note>();

            CreateMap<Note, NoteVm>();
        }
    }
}
