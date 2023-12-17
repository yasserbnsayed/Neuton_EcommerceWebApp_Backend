using Application.Contracts;
using Domain;
using DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly IcountryReposatory _countryRepo;
        private readonly IMapper _mapper;
        public CountryServices(IcountryReposatory _countryRepo,IMapper mapper)
        {
            this._countryRepo = _countryRepo;
            this._mapper = mapper;



        }
        public async Task<List<CountryDTO>> GetAll()
        {
            List<Country> countries = (await _countryRepo.GetAllAsync()).ToList();
            return _mapper.Map<List<CountryDTO>>(countries);
        }
    }
}
