﻿
using DockerAPIS.Core.Models;
using DockerAPIS.Entities;
using DockerAPIS.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Business.Abstract
{
    public interface IContactService
    {
        Task<GetManyResult<Contact>> GetAllAsync();
        Task<GetOneResult<ContactDto>> GetAsync(string id);
        Task<GetOneResult<Contact>> CreateAsync(string id, string name, DateTime dateOfBirth);
    }
}