using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWP_Final.Entities;
using SWP_Final.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SWP_Final.Repositories
{
    public class AgencyRepositories : IAgencyRepositories
    {
        private readonly RealEasteSWPContext _context;
        private readonly IMapper _mapper;

        public AgencyRepositories(RealEasteSWPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AgencyModel>> GetAllAgenciesAsync()
        {
            var agencies = await _context.Agencies.ToListAsync();
            return _mapper.Map<List<AgencyModel>>(agencies);
        }

        public async Task<AgencyModel> GetAgencyByIdAsync(string id)
        {
            var agency = await _context.Agencies.FindAsync(id);
            return _mapper.Map<AgencyModel>(agency);
        }

        public async Task AddAgencyAsync(AgencyModel agency)
        {
            var newAgency = _mapper.Map<Agency>(agency);
            _context.Agencies.Add(newAgency);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAgencyAsync(string id, AgencyModel agencyModel)
        {
            var agency = await _context.Agencies.FindAsync(id);
            if (agency != null)
            {
                _mapper.Map(agencyModel, agency);
                _context.Agencies.Update(agency);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAgencyAsync(string id)
        {
            var agency = await _context.Agencies.FindAsync(id);
            if (agency != null)
            {
                _context.Agencies.Remove(agency);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> UploadImageAsync(string agencyId, IFormFile file)
        {
            var agency = await _context.Agencies.FindAsync(agencyId);
            if (agency == null)
            {
                throw new ArgumentException("Agency not found.");
            }

            var fileName = $"Images/Agencies/{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            agency.Images = fileName;
            await _context.SaveChangesAsync();

            return fileName;
        }

        public async Task<string> GetImageAsync(string agencyId)
        {
            var agency = await _context.Agencies.FindAsync(agencyId);
            if (agency == null || string.IsNullOrEmpty(agency.Images))
            {
                throw new ArgumentException("Image not found.");
            }

            return agency.Images;
        }

        public async Task DeleteImageAsync(string agencyId)
        {
            var agency = await _context.Agencies.FindAsync(agencyId);
            if (agency == null || string.IsNullOrEmpty(agency.Images))
            {
                throw new ArgumentException("Image not found.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", agency.Images);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                agency.Images = null;
                await _context.SaveChangesAsync();
            }
        }
    }
}
