using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tutoronic.Converter;
using Tutoronic.Models;
using Tutoronic.Response;
using Tutoronic.Services.Interface;
using Tutoronic.ViewModels;

namespace Tutoronic.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly Model1 _dbContext;
        private readonly AdminConverter _adminConverter;
        public AdminService(Model1 dbContext)
        {
            _dbContext = dbContext;
            _adminConverter = new AdminConverter();
        }
        public async Task<GetAllAdminsResponse> GetAllAdmins()
        {
            var adminResponse = new GetAllAdminsResponse();
            var adminEntities = await _dbContext.Admins.ToListAsync();
            adminResponse.AllAdmin = _adminConverter.ConvertAdminEntitiesToModel(adminEntities);
            return adminResponse;
        }

        public async Task<GetAdminByIdResponse> GetAdminById(int? id)
        {
            var adminResponse = new GetAdminByIdResponse();
            var adminEntity = await GetAdminEntityById(id);
            if (adminEntity != null)
                adminResponse.Admin = _adminConverter.ConvertAdminEntityToModel(adminEntity);

            return adminResponse;
        }

        public async Task<bool> CreateNewAdmin(Admin admin, string adminImagePath)
        {
            try
            {
                var adminCount = await AdminCount(admin.admin_email);
                if (adminCount > 0)
                {
                    return false;
                }
                admin.admin_pic = adminImagePath;
                _dbContext.Admins.Add(admin);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAdmin(AdminVM admin)
        {
            try
            {
                var adminEntity = await GetAdminEntityById(admin.AdminId);
                if (adminEntity == null)
                    return false;

                _adminConverter.UpdateAdminSuccessfully(adminEntity, admin);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            try
            {
                var adminEntity = await GetAdminEntityById(id);
                if (adminEntity == null)
                    return false;

                _dbContext.Admins.Remove(adminEntity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Admin> Login(Admin admin)
        {
            var test = await _dbContext.Admins.FirstOrDefaultAsync(x => x.admin_email == admin.admin_email & x.admin_password == admin.admin_password);
            return test;
        }

        public async Task<bool> AdminApproveCourse(int id, bool isApprove)
        {
            var courseEntity = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Course_id == id);
            courseEntity.approve = isApprove;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<int> AdminCount(string adminEmail)
        {
            return await _dbContext.Admins.CountAsync(a => a.admin_email == adminEmail);
        }

        private async Task<Admin> GetAdminEntityById(int? adminId)
        {
            return await _dbContext.Admins.FirstOrDefaultAsync(a => a.Admin_id == adminId);
        }
    }
}