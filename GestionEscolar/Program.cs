using System;
using GestionEscolar.Models;
using GestionEscolar.Services;
using GestionEscolar.Services.Reportes;

namespace GestionEscolar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var escuela = new EscuelaService();

           
            var admin = new Admin("A1", "Administrador");
            var prof1 = new Profesor("TDS-006", "Francis Ramirez", "Programacion 2");
            var prof2 = new Profesor("TDS-201", "Carlos Ogando", "Inteligencia Artificial");
            var est1 = new Estudiante("E1", "Kemil Martínez", "MAT-001");
            var est2 = new Estudiante("E2", "Ana Rodríguez", "MAT-002");

            escuela.RegistrarUsuario(admin);
            escuela.RegistrarUsuario(prof1);
            escuela.RegistrarUsuario(prof2);
            escuela.RegistrarUsuario(est1);
            escuela.RegistrarUsuario(est2);

            escuela.CrearCurso("TDS-006", "Programacion 2", prof1);
            escuela.CrearCurso("TDS-201", "Inteligencia Artificial", prof2);

            
            IReporte repProm = new ReportePromedios(escuela);
            IReporte repCursos = new ReporteCursosProfesor(escuela);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN ESCOLAR ===");
                Console.WriteLine("1) Listar usuarios");
                Console.WriteLine("2) Listar cursos");
                Console.WriteLine("3) Ver perfil de usuario");
                Console.WriteLine("4) Inscribir estudiante en curso");
                Console.WriteLine("5) Registrar nota");
                Console.WriteLine("6) Ver cursos de un estudiante");
                Console.WriteLine("7) Reporte de promedios");
                Console.WriteLine("8) Reporte de cursos por profesor");
                Console.WriteLine("0) Salir");
                Console.Write("\nSeleccione: ");

                var op = Console.ReadLine();

                try
                {
                    switch (op)
                    {
                        case "1":
                            Console.Clear();
                            foreach (var u in escuela.Usuarios)
                                Console.WriteLine($"{u.GetType().Name} => {u}");
                            Pausa();
                            break;

                        case "2":
                            Console.Clear();
                            foreach (var c in escuela.Cursos)
                                Console.WriteLine(c);
                            Pausa();
                            break;

                        case "3":
                            Console.Clear();
                            Console.Write("ID de usuario: ");
                            var idU = Console.ReadLine();

                            
                            var usuario = BuscarUsuarioPorId(escuela, idU);
                            Console.WriteLine(usuario.MostrarPerfil());
                            Pausa();
                            break;

                        case "4":
                            Console.Clear();
                            Console.Write("ID estudiante (ej E1): ");
                            var idE = Console.ReadLine();
                            Console.Write("Código curso (ej MAT101): ");
                            var cod = Console.ReadLine();

                            escuela.Inscribir(idE, cod);
                            Console.WriteLine("Inscripción realizada.");
                            Pausa();
                            break;

                        case "5":
                            Console.Clear();
                            Console.Write("ID estudiante: ");
                            var eId = Console.ReadLine();
                            Console.Write("Código curso: ");
                            var cCod = Console.ReadLine();
                            Console.Write("Nota (0-100): ");
                            var notaTxt = Console.ReadLine();
                            Console.Write("Descripción (Ej: Parcial 1): ");
                            var desc = Console.ReadLine();

                            if (!double.TryParse(notaTxt, out var nota))
                                throw new ArgumentException("Nota inválida.");

                            escuela.RegistrarNota(eId, cCod, nota, desc);
                            Console.WriteLine("Nota registrada.");
                            Pausa();
                            break;

                        case "6":
                            Console.Clear();
                            Console.Write("ID estudiante: ");
                            var estId = Console.ReadLine();
                            var cursos = escuela.CursosDeEstudiante(estId);

                            Console.WriteLine("Cursos:");
                            foreach (var c in cursos)
                                Console.WriteLine($"- {c.Codigo} {c.Nombre}");

                            Console.WriteLine($"\nPromedio general: {escuela.PromedioEstudiante(estId)}");
                            Pausa();
                            break;

                        case "7":
                            Console.Clear();
                            Console.WriteLine(repProm.Generar());
                            Pausa();
                            break;

                        case "8":
                            Console.Clear();
                            Console.WriteLine(repCursos.Generar());
                            Pausa();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Opción inválida.");
                            Pausa();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nERROR: {ex.Message}");
                    Pausa();
                }
            }
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresiona ENTER para continuar...");
            Console.ReadLine();
        }

        static Usuario BuscarUsuarioPorId(EscuelaService escuela, string? id)
        {
            id = (id ?? "").Trim().ToUpperInvariant();
            foreach (var u in escuela.Usuarios)
                if (u.Id == id) return u;

            throw new InvalidOperationException("Usuario no encontrado.");
        }
    }
}
