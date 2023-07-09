using System.Collections.Generic;
using Tutoronic.Models;
using Tutoronic.ViewModels;

namespace Tutoronic.Converter
{
    public class AdminConverter
    {
        public List<AdminVM> ConvertAdminEntitiesToModel(List<Admin> adminEntities)
        {
            var adminList = new List<AdminVM>();
            foreach (var adminEntity in adminEntities)
            {
                var admin = new AdminVM()
                {
                    AdminId = adminEntity.Admin_id,
                    AdminAddress = adminEntity.admin_adress,
                    AdminContact = adminEntity.admin_contact,
                    AdminEmail = adminEntity.admin_email,
                    AdminName = adminEntity.admin_name,
                    AdminPassword = adminEntity.admin_password,
                    AdminPic = adminEntity.admin_pic,
                };
                adminList.Add(admin);
            }
            return adminList;
        }

        public AdminVM ConvertAdminEntityToModel(Admin adminEntity)
        {
            return new AdminVM()
            {
                AdminId = adminEntity.Admin_id,
                AdminAddress = adminEntity.admin_adress,
                AdminContact = adminEntity.admin_contact,
                AdminEmail = adminEntity.admin_email,
                AdminName = adminEntity.admin_name,
                AdminPassword = adminEntity.admin_password,
                AdminPic = adminEntity.admin_pic,
            };
        }

        public void UpdateAdminSuccessfully(Admin adminEntity, AdminVM request)
        {
            adminEntity.admin_adress = request.AdminAddress;
            adminEntity.admin_contact = request.AdminContact;
            adminEntity.admin_email = request.AdminEmail;
            adminEntity.admin_name = request.AdminName;
            adminEntity.admin_password = request.AdminPassword;
            if (request.AdminPic != null)
                adminEntity.admin_pic = request.AdminPic;
        }
    }
}