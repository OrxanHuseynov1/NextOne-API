using AutoMapper;
using BusinessLayer.DTOs.Chat;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ChatService : IChatService
{
    private readonly IChatReadRepository _chatReadRepository;
    private readonly IChatWriteRepository _chatWriteRepository;
    private readonly IMapper _mapper;

    public ChatService(IChatReadRepository chatReadRepository, IChatWriteRepository chatWriteRepository, IMapper mapper)
    {
        _chatReadRepository = chatReadRepository;
        _chatWriteRepository = chatWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateChatAsync(ChatPostDTO chatPostDTO)
    {
        Chat chat = _mapper.Map<Chat>(chatPostDTO);

        await _chatWriteRepository.CreateAsync(chat);
        var result = await _chatWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Chat could not be created.");
        }
    }

    public async Task DeleteChatAsync(Guid id)
    {
        if (!await _chatReadRepository.IsExist(id)) throw new Exception("Chat not found.");
        Chat chat = await _chatReadRepository.GetByIdAsync(id) ?? throw new Exception("Chat not found.");

        _chatWriteRepository.Delete(chat);

        var result = await _chatWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Chat could not be deleted.");
        }
    }

    public async Task SoftDeleteChatAsync(Guid id)
    {
        if (!await _chatReadRepository.IsExist(id)) throw new Exception("Chat not found.");
        Chat chat = await _chatReadRepository.GetOneByCondition(c => c.Id == id && !c.IsDeleted, false)
                            ?? throw new Exception("Chat not found.");
        chat.IsDeleted = true;
        _chatWriteRepository.Update(chat);

        var result = await _chatWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Chat could not be soft deleted.");
        }
    }

    public async Task RestoreChatAsync(Guid id)
    {
        if (!await _chatReadRepository.IsExist(id)) throw new Exception("Chat not found.");
        Chat chat = await _chatReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                            ?? throw new Exception("Chat not found.");
        chat.IsDeleted = false;
        _chatWriteRepository.Update(chat);

        var result = await _chatWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Chat could not be restored.");
        }
    }

    public async Task<ICollection<ChatGetDTO>> GetAllSoftDeletedChat()
    {
        ICollection<Chat> chats = await _chatReadRepository.GetAllByCondition(c => c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ChatGetDTO>>(chats);
    }

    public async Task<ICollection<ChatGetDTO>> GetAllActiveChatAsync()
    {
        ICollection<Chat> chats = await _chatReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ChatGetDTO>>(chats);
    }

    public async Task<ChatGetDTO> GetByIdChatAsync(Guid id)
    {
        if (!await _chatReadRepository.IsExist(id)) throw new Exception("Chat not found.");
        Chat chat = await _chatReadRepository.GetByIdAsync(id) ?? throw new Exception("Chat not found.");
        return _mapper.Map<ChatGetDTO>(chat);
    }
}