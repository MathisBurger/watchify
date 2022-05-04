namespace watchify.Shared;

public class DbAccess
{

    private readonly DatabaseContext Ctx;

    public DbAccess(DatabaseContext ctx)
    {
        Ctx = ctx;
    }

}