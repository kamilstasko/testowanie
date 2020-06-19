using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.MenuItems;
using System.Collections.Generic;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.InputDevices;

namespace UITest
{
    [TestClass]
    public class TestID01
    {
        private Application App;
        private readonly string appPath = @"C:\WINDOWS\system32\notepad.exe";
        private const string APP_TITLE = "Bez tytułu — Notatnik";
        private const string Tekst = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed condimentum dapibus maximus. Vivamus nec luctus sapien. Curabitur laoreet ultricies velit, dapibus dignissim ligula sit pretium semper.";
        private const string TekstWynikowy = "Lorem ipsum dolor kotek amet, consectetur adipiscing elit. Sed condimentum dapibus maximus. Vivamus nec luctus sapien. Curabitur laoreet ultricies velit, dapibus dignissim ligula kotek pretium semper.";
        private const int TimeSleep = 1000;

        [TestMethod]
        public void Run()
        {
            App = Application.Launch(appPath);
            Thread.Sleep(TimeSleep);
            
            Window window = App.GetWindow(APP_TITLE);
            Assert.AreEqual(APP_TITLE, window.Title, "Asercja 1: Sprawdzenie tytułu aplikacji");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria1 = SearchCriteria.ByText("Edytor tekstów");
            TextBox Edytor = window.Get<TextBox>(searchCriteria1);
            Edytor.BulkText = Tekst;
            Assert.AreEqual(Tekst, Edytor.BulkText, "Asercja 2: Sprawdzenie wpisania tekstu");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria2 = SearchCriteria.ByText("Edycja");
            Menu Edycja = window.Get<Menu>(searchCriteria2);
            Edycja.Click();
            Assert.AreEqual(true, Edycja.IsFocussed, "Asercja 3: Sprawdzenie rozwinięcia opcji Edycja");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria3 = SearchCriteria.ByText("Zamień...");
            Menu Zamien = window.Get<Menu>(searchCriteria3);
            Zamien.Click();
            Window childWindow = window.ModalWindow("Zamienianie");
            Assert.AreEqual("Zamienianie", childWindow.Title, "Asercja 4: Sprawdzenie tytułu okna Zamienianie");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria4 = SearchCriteria.ByText("Znajdź:");
            TextBox Znajdz = childWindow.Get<TextBox>(searchCriteria4);
            Znajdz.BulkText = "sit";
            Assert.AreEqual("sit", Znajdz.BulkText, "Asercja 5: Sprawdzenie wpisania tekstu sit do pola Znajdź");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria5 = SearchCriteria.ByText("Zamień na:");
            TextBox ZamienNa = childWindow.Get<TextBox>(searchCriteria5);
            ZamienNa.BulkText = "kotek";
            Assert.AreEqual("kotek", ZamienNa.BulkText, "Asercja 6: Sprawdzenie wpisania tekstu kotek do pola Zamień na");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria6 = SearchCriteria.ByText("Zamień wszystko");
            Button ZamienWszystko = childWindow.Get<Button>(searchCriteria6);
            ZamienWszystko.Click();
            SearchCriteria searchCriteria7 = SearchCriteria.ByText("Zamknij");
            Button ExitZamienianie = childWindow.Get<Button>(searchCriteria7);
            ExitZamienianie.Click();
            Assert.AreEqual(TekstWynikowy, Edytor.BulkText, "Asercja 7: Sprawdzenie zamienionego tekstu");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria8 = SearchCriteria.ByText("Zamknij");
            Button Exit = window.Get<Button>(searchCriteria8);
            Exit.Click();
            Window dialogWindow = window.ModalWindow("Notatnik");
            Assert.AreEqual("Notatnik", dialogWindow.Title, "Asercja 8: Sprawdzenie tytułu okna dialogowego zapisu przed zamknięciem");
            Thread.Sleep(TimeSleep);
            
            SearchCriteria searchCriteria9 = SearchCriteria.ByText("Nie zapisuj");
            Button NoSave = dialogWindow.Get<Button>(searchCriteria9);
            NoSave.Click();
            Thread.Sleep(TimeSleep);

            Assert.AreEqual(true, window.IsClosed, "Asercja 9: Sprawdzenie czy aplikacja została zamknięta");
        }
    }

    [TestClass]
    public class TestID09
    {
        private Application App;
        private readonly string appPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe";
        private const string APP_TITLE = "Strona początkowa - Microsoft Visual Studio ";
        private const int TimeSleep = 1000;

        [TestMethod]
        public void Run()
        {
            App = Application.Launch(appPath);
            Thread.Sleep(TimeSleep);

            Window window = App.GetWindow(APP_TITLE);
            Assert.AreEqual(APP_TITLE, window.Title, "Asercja 01: Sprawdzenie tytułu aplikacji");
            Thread.Sleep(TimeSleep);
            
            SearchCriteria searchCriteria2 = SearchCriteria.ByText("Plik");
            Menu Plik = window.Get<Menu>(searchCriteria2);
            Plik.Click();
            Assert.AreEqual(true, Plik.IsFocussed, "Asercja 02: Sprawdzenie rozwinięcia opcji Plik");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria3 = SearchCriteria.ByText("Nowy");
            Menu Nowy = window.Get<Menu>(searchCriteria3);
            Nowy.Click();
            Assert.AreEqual(true, Nowy.IsFocussed, "Asercja 03: Sprawdzenie rozwinięcia opcji Nowy");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria4 = SearchCriteria.ByText("Projekt...");
            Menu Projekt = window.Get<Menu>(searchCriteria4);
            Projekt.Click();
            Window childWindow = window.ModalWindow("Nowy projekt");        
            Assert.AreEqual("Nowy projekt", childWindow.Title, "Asercja 04: Sprawdzenie tytułu okna Nowy projekt");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria5 = SearchCriteria.ByText("Wyszukaj (Ctrl+E)");
            TextBox Wyszukaj = childWindow.Get<TextBox>(searchCriteria5);
            Wyszukaj.Click();
            Assert.AreEqual(true, Wyszukaj.IsFocussed, "Asercja 05: Sprawdzenie czy pole Wyszukaj zostało aktywowane");
            Thread.Sleep(TimeSleep);

            Wyszukaj.BulkText = "Aplikacja klasyczna systemu Windows";
            Assert.AreEqual("Aplikacja klasyczna systemu Windows", Wyszukaj.BulkText, "Asercja 06: Sprawdzenie wpisania tekstu");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria6 = SearchCriteria.ByText("Aplikacja klasyczna systemu Windows");
            Label Wybor = childWindow.Get<Label>(searchCriteria6);
            Wybor.Click();
            Assert.AreEqual(true, Wybor.GetParent<ListItem>().IsFocussed, "Asercja 07: Sprawdzenie czy podświetlono opcje");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria7 = SearchCriteria.ByText("Nazwa:");
            TextBox Nazwa = childWindow.Get<TextBox>(searchCriteria7);
            Nazwa.Click();
            Assert.AreEqual(true, Nazwa.IsFocussed, "Asercja 08: Sprawdzenie czy pole Nazwa zostało aktywowane");
            Thread.Sleep(TimeSleep);

            Nazwa.BulkText = "Projekt testowy";
            Assert.AreEqual("Projekt testowy", Nazwa.BulkText, "Asercja 09: Sprawdzenie wpisania tekstu");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria8 = SearchCriteria.ByText("OK");
            Button Ok = childWindow.Get<Button>(searchCriteria8);
            Ok.Click();
            SearchCriteria searchCriteria9 = SearchCriteria.ByText("Projekt testowy.cpp");
            Label Wynik = window.Get<Label>(searchCriteria9);
            Assert.AreEqual("Projekt testowy.cpp", Wynik.Text, "Asercja 10: Sprawdzenie czy otworzono kartę Projekt testowy.cpp");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria10 = SearchCriteria.ByText("Zamknij");
            Button Exit = window.Get<Button>(searchCriteria10);
            Exit.Click();
            Thread.Sleep(TimeSleep);

            Assert.AreEqual(true, window.IsClosed, "Asercja 11: Sprawdzenie czy aplikacja została zamknięta");
        }
    }

    [TestClass]
    public class TestID10
    {
        private Application App;
        private readonly string appPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe";
        private const string APP_TITLE = "Strona początkowa - Microsoft Visual Studio ";
        private const int TimeSleep = 1000;

        [TestMethod]
        public void Run()
        {
            App = Application.Launch(appPath);
            Thread.Sleep(TimeSleep);

            Window window = App.GetWindow(APP_TITLE);
            Assert.AreEqual(APP_TITLE, window.Title, "Asercja 01: Sprawdzenie tytułu aplikacji");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria2 = SearchCriteria.ByText("Plik");
            Menu Plik = window.Get<Menu>(searchCriteria2);
            Plik.Click();
            Assert.AreEqual(true, Plik.IsFocussed, "Asercja 02: Sprawdzenie rozwinięcia opcji Plik");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria3 = SearchCriteria.ByText("Niedawno używane projekty i rozwiązania");
            Menu Ostatni = window.Get<Menu>(searchCriteria3);
            Ostatni.Click();
            Assert.AreEqual(true, Ostatni.IsFocussed, "Asercja 03: Sprawdzenie rozwinięcia opcji");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria4 = SearchCriteria.ByText(@"1 Projekt testowy.sln (C:\Users\Kamil\source\repos\Projekt testowy)");
            Menu Projekt = window.Get<Menu>(searchCriteria4);
            Projekt.Click();
            Assert.AreEqual("Projekt testowy - Microsoft Visual Studio ", window.Title, "Asercja 04: Sprawdzenie tytułu okna projektu");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria5 = SearchCriteria.ByText("Projekt");
            Menu ProjektW = window.Get<Menu>(searchCriteria5);
            ProjektW.Click();
            Assert.AreEqual(true, ProjektW.IsFocussed, "Asercja 05: Sprawdzenie rozwinięcia opcji Projekt");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria6 = SearchCriteria.ByText("Dodaj nowy element...");
            Menu Dodaj = window.Get<Menu>(searchCriteria6);
            Dodaj.Click();
            Window childWindow = window.ModalWindow("Dodaj nowy element - Projekt testowy");
            Assert.AreEqual("Dodaj nowy element - Projekt testowy", childWindow.Title, "Asercja 06: Sprawdzenie tytułu okna Dodaj nowy element");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria7 = SearchCriteria.ByText("Plik C++ (.cpp)");
            Label Wybor = childWindow.Get<Label>(searchCriteria7);
            Wybor.Click();
            Assert.AreEqual(true, Wybor.GetParent<ListItem>().IsFocussed, "Asercja 07: Sprawdzenie czy podświetlono opcje");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria8 = SearchCriteria.ByText("Nazwa:");
            TextBox Nazwa = childWindow.Get<TextBox>(searchCriteria8);
            Nazwa.Click();
            Assert.AreEqual(true, Nazwa.IsFocussed, "Asercja 08: Sprawdzenie czy pole Nazwa zostało aktywowane");
            Thread.Sleep(TimeSleep);

            Nazwa.BulkText = "Plik testowy.cpp";
            Assert.AreEqual("Plik testowy.cpp", Nazwa.BulkText, "Asercja 09: Sprawdzenie wpisania tekstu");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria9 = SearchCriteria.ByText("Dodaj");
            Button DodajP = childWindow.Get<Button>(searchCriteria9);
            DodajP.Click();
            SearchCriteria searchCriteria10 = SearchCriteria.ByText("Plik testowy.cpp");
            Label Wynik = window.Get<Label>(searchCriteria10);
            Assert.AreEqual("Plik testowy.cpp", Wynik.Text, "Asercja 10: Sprawdzenie czy otworzono kartę Plik testowy.cpp");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria11 = SearchCriteria.ByText("Zamknij");
            Button Exit = window.Get<Button>(searchCriteria11);
            Exit.Click();
            Window dialogWindow = window.ModalWindow("Microsoft Visual Studio");
            Assert.AreEqual("Microsoft Visual Studio", dialogWindow.Title, "Asercja 11: Sprawdzenie tytułu okna dialogowego zapisu przed zamknięciem");
            Thread.Sleep(TimeSleep);

            SearchCriteria searchCriteria12 = SearchCriteria.ByText("Tak");
            Button NoSave = dialogWindow.Get<Button>(searchCriteria12);
            NoSave.Click();
            Thread.Sleep(TimeSleep);

            Assert.AreEqual(true, window.IsClosed, "Asercja 12: Sprawdzenie czy aplikacja została zamknięta");        
        }
    }
}
