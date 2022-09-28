namespace WebAPIOrderTracking.Guards.Interfaces
{
    public interface IHasherable
    {
        string Hash(string secret);

        bool Verify(string secret, string hash);
    }
}
