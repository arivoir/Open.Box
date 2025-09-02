using DotNetEnv;
using Open.IO;
using System.Text;

namespace Open.Box.Test;

public class Tests
{
    private string _accessToken;
    private string _rootFolderId;

    [SetUp]
    public async Task Setup()
    {
        Env.Load();
        var clientId = Environment.GetEnvironmentVariable("CLIENT_ID")!;
        var clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET")!;
        var passphrase = Environment.GetEnvironmentVariable("PASSFRASE")!;
        var PUBLIC_KEY_ID = Environment.GetEnvironmentVariable("PUBLIC_KEY_ID")!;
        var enterpriseId = Environment.GetEnvironmentVariable("ENTERPRISE_ID")!;
        var privateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY")!.Replace("\\n", Environment.NewLine);
        var token = await BoxClient.ExchangeJWTForAccessTokenAsync(clientId, clientSecret, enterpriseId, privateKey, passphrase, CancellationToken.None);
        _accessToken = token.AccessToken;
        var client = new BoxClient(_accessToken);
        var rootFolderName = Guid.NewGuid().ToString();
        _rootFolderId = rootFolderName;
        var folder = new Item
        {
            Name = rootFolderName,
        };
        await client.CreateFolderAsync(folder, rootFolderName);
    }

    [Test]
    public async Task GetItemsTest()
    {
        var stringToUpload = "Hello, World!";
        var client = new BoxClient(_accessToken);
        var file = await client.UploadFileAsync(new Item { Name = "file.txt", Parent = new Item { Id = _rootFolderId } }, new MemoryStream(Encoding.UTF8.GetBytes(stringToUpload)), new Progress<StreamProgress>(p => { }), CancellationToken.None);
        var folder = await client.CreateFolderAsync(new Item { Name = "folder", Parent = new Item { Id = _rootFolderId } });
        var items = await client.GetFolderItemsAsync(_rootFolderId);

        Assert.That(items.Entries, Is.Not.Null);
        Assert.That(items.Entries.Count, Is.EqualTo(2));
        Assert.That(items.Entries[0].Name, Is.EqualTo("folder"));
        Assert.That(items.Entries[1].Name, Is.EqualTo("file.txt"));
    }
}
