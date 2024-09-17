using AutoMapper;
using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>();

        CreateMap<CreateUserCommand, User>()
            .ForMember(x => x.Password, y => y.MapFrom(z => PasswordEncryptor.Encrypt(z.Password)));

        CreateMap<UpdateUserCommand, User>();

        CreateMap<User, UserDetailViewModel>(); 

        CreateMap<CreateEntryCommand, Entry>();

        CreateMap<CreateEntryCommentCommand, EntryComment>();

        CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));
    }
}
