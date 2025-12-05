using NUnit.Framework;
using BLE_vaegt_app.viewmodel;       // Namespace for testprojektet

namespace UnitTestLogIn
{
    [TestFixture]               // Marker klassen som en NUnit testklasse
    public class UnitTest1
    {
        private LoginViewModel login;    // Objekt af Login-klassen, som vi tester

        [SetUp]                 // Kører før hver testmetode
        public void Setup()
        {
            login = new LoginViewModel();     // Initialiserer login-objektet før hver test
        }


        // Happy Path: Gyldigt navn uden specielle tegn
        [Test]    // Marker metoden som en test
        public void Test_ValidName_SetName_ExpectSameName()
        {
            // Act: Sæt et gyldigt navn
            login.Navn = "Anders Andersen";

            // Assert: Tjek at navnet er sat korrekt
            Assert.That("Anders Andersens", Is.EqualTo(login.Navn));
        }


        // Negativt scenarie: Navn indeholder tal og specialtegn
        [Test]     // Marker metoden som en test
        public void Test_InvalidCharactersInName_ExpectFilteredName()
        {
            // Act: Sæt et navn med ugyldige tegn
            login.Navn = "An^^ders123!!! Ølsøn";

            // Assert: Tjek at alle ugyldige tegn er fjernet
            Assert.That("Anders Ølsøn", Is.EqualTo(login.Navn));
        }


        //Edge case: Sætter null, forvent tom streng
        [Test]     // Marker metoden som en test
        public void Test_NullName_ExpectEmptyString()
        {
            // Act: Sæt navnet til null
            login.Navn = null;

            // Assert: Tjek at property returnerer tom streng i stedet for null
            Assert.That("", Is.EqualTo(login.Navn));
        }
    }
}
