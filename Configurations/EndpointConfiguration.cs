using DevConnect.Endpoints;

namespace DevConnect.Configurations;

public static class EndpointConfiguration
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        // Ro'yhatdan o'tish endpointlarini qo'shish
        app.MapAuthEndpoints();
        // Comment endpointlarini qo'shish
        app.MapCommentEndpoints();
        // Follow endpointlarini qo'shish
        app.MapFollowEndpoints();
        // Like endpointlarini qo'shish
        app.MapLikeEndpoints();
        // Xabar endpointlarini qo'shish
        app.MapMessageEndpoints();
        // Malumotlar endpointlarini qo'shish
        app.MapNotificationEndpoints();
        // Postlar endpointlarini qo'shish
        app.MapPostEndpoints();
        // Loyihalar endpointlarini qo'shish
        app.MapProjectEndpoints();
        // Qobiliyatlar endpointlarini qo'shish
        app.MapSkillEndpoints();
        // Foydalanuvchilar endpointlarini qo'shish
        app.MapUserEndpoints();
    }
}
