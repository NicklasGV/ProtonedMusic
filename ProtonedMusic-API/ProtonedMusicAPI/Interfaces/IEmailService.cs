using ProtonedMusicAPI.DTO.EmailDTO;

namespace ProtonedMusicAPI.Interfaces
{
    public interface IEmailService
    {
        void SendEMail(EmailResponse request);
    }
}
