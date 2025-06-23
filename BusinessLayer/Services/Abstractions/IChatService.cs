using BusinessLayer.DTOs.Chat;

namespace BusinessLayer.Services.Abstractions;

public interface IChatService
{
    Task CreateChatAsync(ChatPostDTO ChatPostDTO);
    Task DeleteChatAsync(Guid id);
    Task SoftDeleteChatAsync(Guid id);
    Task RestoreChatAsync(Guid id);
    Task<ICollection<ChatGetDTO>> GetAllSoftDeletedChat();
    Task<ICollection<ChatGetDTO>> GetAllActiveChatAsync();
    Task<ChatGetDTO> GetByIdChatAsync(Guid id);

}