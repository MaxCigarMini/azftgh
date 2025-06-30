using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Console_project.Service;
using Console_project.Repository;
using Console_project.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace Console_project.Service
{
    internal class ConsoleControle
    {
        private static NoteService _noteService = new NoteService();
        private static UserService _userService = new UserService();
        private static User _currentUser;
        public static void RUN()
        {
            MainMenu();
        }

        private static readonly Dictionary<string, Action> Commands = new Dictionary<string, Action>
        {
            { "1", Registration },
            { "2", Login },
            { "3", GetAllNotes },
            { "4", AddNote },
            { "5", ComplitedNote },
            { "6", ChangeNote },
            { "7", RemoveNote },
            { "8", ShowHelp },
            { "9", Exit },
        };

        public static void MainMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("\n===MENU===");
                Console.WriteLine("1) Регистрация");
                Console.WriteLine("2) LOGIN");
                Console.WriteLine("3) Просмотр заметок");
                Console.WriteLine("4) Создать заметку");
                Console.WriteLine("5) Изменить статус заметки");
                Console.WriteLine("6) Редактировать заметку");
                Console.WriteLine("7) Удалить заметку");
                Console.WriteLine("8) Помощь");
                Console.WriteLine("9) Выход");

                Console.WriteLine("Выберите пункт: ");
                string input = Console.ReadLine();

                if (Commands.TryGetValue(input, out Action action))
                {
                    try
                    {
                        action.Invoke();
                        if (input == "9")
                            isRunning = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод! Попробуйте снова.");
                    Console.ReadKey();
                }
            }
        }
        private static void Registration()
        {
            Console.Clear();
            Console.WriteLine("===Регистрация===");
            Console.Write("Введите ваше имя: ");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Имя не может быть пустым");
                Console.ReadKey();
                return;
            }
            try
            {
                _currentUser = _userService.Registration(input);
                Console.WriteLine($"Добро пожаловать, {input}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка регистрации: {ex.Message}");
            }

            Console.ReadKey();
        }
        private static void Login()
        {
            Console.Clear();
            Console.WriteLine("=== Вход ===");
            Console.Write("Введите ваше имя: ");

            string username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Имя не может быть пустым!");
                Console.ReadKey();
                return;
            }

            try
            {
                _currentUser = _userService.GetUserByName(username);

                if (_currentUser == null)
                {
                    Console.WriteLine("Пользователь не найден. Хотите зарегистрироваться? (Да/Нет)");
                    var response = Console.ReadLine();

                    if (response?.ToLower() == "да")
                    {
                        Registration();
                    }
                }
                else
                {
                    Console.WriteLine($"Добро пожаловать, {_currentUser.Name}!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при входе: {ex.Message}");
                Console.ReadKey();
            }

            Console.ReadKey();
        }
        private static void GetAllNotes()
        {
            Console.Clear();
            Console.WriteLine("===Список всех Записок ===");

            if (!CheckUserAuthorization()) return;
            try
            {

                var notes = _noteService.GetAllNotes(_currentUser.UserId);

                if (!notes.Any())
                {
                    Console.WriteLine("У вас пока нет заметок.");
                }
                else
                {
                    foreach (var note in notes)
                    {
                        Console.WriteLine($"ID: {note.NoteId}");
                        Console.WriteLine($"Заголовок: {note.Title}");
                        Console.WriteLine($"Описание: {note.Description}");
                        Console.WriteLine($"Статус: {(note.IsCompleted ? "Завершена" : "Активна")}");
                        Console.WriteLine("-------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении заметок: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void AddNote()
        {
            Console.Clear();
            Console.WriteLine("===Создание заметки===");

            if (!CheckUserAuthorization()) return;

            try
            {
                Console.Write("Введите Заголовок: ");
                string title = Console.ReadLine().Trim();

                Console.Write("Введите Описание: ");
                string description = Console.ReadLine().Trim();

                int NoteId = 0;

                _noteService.AddNote(NoteId, title, description);
                Console.WriteLine("Заметка успешно создана!");
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }

            Console.ReadKey();
        }

        private static void ComplitedNote()
        {
            Console.Clear();
            Console.WriteLine("=== Изменить статус заметки ===");

            if (!CheckUserAuthorization()) return;

            if (!TryReadInt("Введите ID заметки: ", out int id)) return;

            try
            {
                _noteService.CompleteNote(id);
                Console.WriteLine("Статус заметки изменен на 'Завершена'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void ChangeNote()
        {
            Console.Clear();
            Console.WriteLine("Введите ID заметки: ");

            if (!CheckUserAuthorization()) return;

            Console.Write("Введите ID заметки: ");
            if (!TryReadInt("Введите ID заметки: ", out int id)) return;

            Console.Write("Введите новый заголовок: ");
            string title = Console.ReadLine();

            Console.Write("Введите новое описание: ");
            string description = Console.ReadLine();

            try
            {
                _noteService.ChangeNote(id, title, description);
                Console.WriteLine("Заметка успешно изменена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void RemoveNote()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление заметки ===");

            if (!CheckUserAuthorization()) return;

            if (!TryReadInt("Введите ID заметки: ", out int id)) return;

            try
            {
                _noteService.RemoveNote(id);
                Console.WriteLine("Заметка успешно удалена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadKey();
        }

        private static void ShowHelp()
        {
            Console.WriteLine("\n=== Помощь ===");
            Console.WriteLine("1. Регистрация - создание нового пользователя");
            Console.WriteLine("=====Для выполнения действий со 2 по 6, ЗАРЕГИСТРИРУЙТЕСЬ=====");
            Console.WriteLine("2. Просмотр заметок - просмотр всех ваших заметок");
            Console.WriteLine("3. Создать заметку - добавление новой заметки");
            Console.WriteLine("4. Изменить статус - пометить заметку как выполненную");
            Console.WriteLine("5. Редактировать заметку - изменить содержимое заметки");
            Console.WriteLine("6. Удалить заметку - удалить выбранную заметку");
            Console.WriteLine("7. Помощь - ===Вы находитесь в этой точке===");
            Console.WriteLine("8. Выход - завершение работы программы");
        }
        private static void Exit()
        {
            Console.WriteLine("Выход из приложения...");
        }

        private static bool CheckUserAuthorization()
        {
            if (_currentUser != null)
                return true;

            else
            {
                Console.WriteLine("Сначала зарегистрируйтесь!");
                Console.ReadKey();
                return false;
            }

        }
        private static bool TryReadInt(string prompt, out int result, string errorMessage = "Неверный формат числа!")
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out result))
                return true;

            Console.WriteLine(errorMessage);
            Console.ReadKey();
            return false;
        }
    }
}
