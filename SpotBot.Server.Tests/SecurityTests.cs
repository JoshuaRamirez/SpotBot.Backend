using System;
using SpotBot.Server.Core;

[TestFixture]
public class SecurityTests
{
    [Test]
    public void TestObfuscationAndDeobfuscation()
    {
        string key = Encryption.GenerateSymmetricKey();
        string obfuscated = Obfuscation.Obfuscate(key);
        string deobfuscated = Obfuscation.Deobfuscate(obfuscated);
        Assert.That(deobfuscated, Is.EqualTo(key));
    }
    [Test]
    public void TestEncryptionAndDecryption()
    {
        var value = "ASDFasdf!@#$1234-=_+";
        string key = Encryption.GenerateSymmetricKey();
        var encryption = new Encryption(key);
        var encrypted = encryption.Encrypt(value);
        var decrypted = encryption.Decrypt(encrypted);
        Assert.That(value, Is.EqualTo(decrypted));
    }
}
