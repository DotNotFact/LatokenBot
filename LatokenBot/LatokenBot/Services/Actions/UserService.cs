using LatokenBot.Data;
using LatokenBot.Entities;
using MongoDB.Driver;

namespace LatokenBot.Services.Actions;

internal class UserService(MongoDatabase userDatabase)
{
    private readonly MongoDatabase _userDatabase = userDatabase;
    private const string UserNameCollection = "users";

    public IMongoCollection<UserEntity> GetAllUsers()
    {
        var db = userDatabase.GetDatabase();
        var users = db.GetCollection<UserEntity>(UserNameCollection);

        return users;
    }

    public async Task<UserEntity> AddAndGetUserByTelegramId(long telegramId, string firstName = "", string lastName = "")
    {
        var collection = GetAllUsers(); 
        var user = await collection.Find(u => u.TelegramId == telegramId).FirstOrDefaultAsync();

        if (user is null)
        {
            user = new UserEntity
            {
                TelegramId = telegramId,
                FirstName = firstName,
                LastName = lastName,
                ChatHistory = []
            };
            await collection.InsertOneAsync(user);
        }

        return user;
    }

    public async Task SaveUser(UserEntity user)
    {
        var collection = GetAllUsers();

        var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, user.Id);
        var update = Builders<UserEntity>.Update.Set(u => u.ChatHistory, user.ChatHistory);

        await collection.UpdateOneAsync(filter, update);
    }
}