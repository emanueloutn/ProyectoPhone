using Xunit;
namespace ProyectoPhoneTest;

public class UnitTest1
{
    [Fact]
    public void Decodificar_InputBasico_RetornaLetraA()
    {
        // Arrange
        var input = "2#";
        
        // Act
        var result = ProyectoPhone.PhoneDecoder.Decodificar(input);
        
        // Assert
        Assert.Equal("A", result);
    }

    [Theory]
    [InlineData("33#", "E")]
    [InlineData("227*#", "B")]
    [InlineData("4433555 555666#", "HELLO")]
    public void Decodificar_CasosDePrueba_RetornaResultadoEsperado(string input, string expected)
    {
        // Act
        var result = ProyectoPhone.PhoneDecoder.Decodificar(input);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Decodificar_InputInvalido_IgnoraCaracteresNoMapeados()
    {
        // Arrange
        var input = "2Z4#";
        
        // Act
        var result = ProyectoPhone.PhoneDecoder.Decodificar(input);
        
        // Assert
        Assert.Equal("A", result); // El "Z" debería ignorarse
    }
    private class FactAttribute : Attribute
    {
    }
}
