using PasswordManager.Models;
using PasswordManager.Models.Dtos;

namespace PasswordManager.Helper
{
    public static class Mapper
    {
        public static PasswordStoreItemDto ToDto(this PasswordStoreItem passwordStoreItem)
        {
            var dto = new PasswordStoreItemDto
            {
                Id = passwordStoreItem.Id,
                App = passwordStoreItem.App,
                Category = passwordStoreItem.Category,
                UserName = passwordStoreItem.UserName,
                Password = passwordStoreItem.EncryptedPassword
            };

            return dto;
        }

        public static PasswordStoreItem ToNonDto(this PasswordStoreItemDto passwordStoreItemDto)
        {
            var passwordStoreItem = new PasswordStoreItem
            {
                Id = passwordStoreItemDto.Id,
                App = passwordStoreItemDto.App,
                Category = passwordStoreItemDto.Category,
                UserName = passwordStoreItemDto.UserName,
                EncryptedPassword = passwordStoreItemDto.Password
            };

            return passwordStoreItem;
        }

        public static void MapValues(this PasswordStoreItemDto sourceObject, PasswordStoreItem destinationObject)
        {
            destinationObject.Id = sourceObject.Id;
            destinationObject.App = sourceObject.App;
            destinationObject.Category = sourceObject.Category;
            destinationObject.UserName = sourceObject.UserName;
            destinationObject.EncryptedPassword = sourceObject.Password;
        }
    }
}
