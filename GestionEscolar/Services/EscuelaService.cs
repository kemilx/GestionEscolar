using System;
using System.Collections.Generic;
using System.Linq;
using GestionEscolar.Models;

namespace GestionEscolar.Services
{
    public class EscuelaService
    {
        private readonly List<Usuario> _usuarios = new();
        private readonly List<Curso> _cursos = new();
        private readonly List<Inscripcion> _inscripciones = new();
        private readonly List<Evaluacion> _evaluaciones = new();

        public IReadOnlyList<Usuario> Usuarios => _usuarios;
        public IReadOnlyList<Curso> Cursos => _cursos;
        public IReadOnlyList<Inscripcion> Inscripciones => _inscripciones;
        public IReadOnlyList<Evaluacion> Evaluaciones => _evaluaciones;

        // Usuarios
        public void RegistrarUsuario(Usuario u) => _usuarios.Add(u ?? throw new ArgumentNullException(nameof(u)));

        public Estudiante BuscarEstudiante(string id)
        {
            id = (id ?? "").Trim().ToUpperInvariant();
            var e = _usuarios.OfType<Estudiante>().FirstOrDefault(x => x.Id == id);
            return e ?? throw new InvalidOperationException("Estudiante no encontrado.");
        }

        public Profesor BuscarProfesor(string id)
        {
            id = (id ?? "").Trim().ToUpperInvariant();
            var p = _usuarios.OfType<Profesor>().FirstOrDefault(x => x.Id == id);
            return p ?? throw new InvalidOperationException("Profesor no encontrado.");
        }

        // Cursos
        public void CrearCurso(string codigo, string nombre, Profesor profesor)
        {
            if (_cursos.Any(c => c.Codigo == codigo.Trim().ToUpperInvariant()))
                throw new InvalidOperationException("Ya existe un curso con ese código.");

            _cursos.Add(new Curso(codigo, nombre, profesor));
        }

        public Curso BuscarCurso(string codigo)
        {
            codigo = (codigo ?? "").Trim().ToUpperInvariant();
            var c = _cursos.FirstOrDefault(x => x.Codigo == codigo);
            return c ?? throw new InvalidOperationException("Curso no encontrado.");
        }

        // Inscripciones
        public void Inscribir(string estudianteId, string cursoCodigo)
        {
            var e = BuscarEstudiante(estudianteId);
            var c = BuscarCurso(cursoCodigo);

            bool ya = _inscripciones.Any(i => i.Estudiante.Id == e.Id && i.Curso.Codigo == c.Codigo);
            if (ya) throw new InvalidOperationException("El estudiante ya está inscrito en ese curso.");

            _inscripciones.Add(new Inscripcion(e, c));
        }

        public IEnumerable<Curso> CursosDeEstudiante(string estudianteId)
        {
            var e = BuscarEstudiante(estudianteId);
            return _inscripciones
                .Where(i => i.Estudiante.Id == e.Id)
                .Select(i => i.Curso)
                .Distinct()
                .ToList();
        }

        public void RegistrarNota(string estudianteId, string cursoCodigo, double nota, string descripcion)
        {
            var e = BuscarEstudiante(estudianteId);
            var c = BuscarCurso(cursoCodigo);

            bool inscrito = _inscripciones.Any(i => i.Estudiante.Id == e.Id && i.Curso.Codigo == c.Codigo);
            if (!inscrito) throw new InvalidOperationException("El estudiante no está inscrito en ese curso.");

            _evaluaciones.Add(new Evaluacion(e, c, nota, descripcion));
        }

        public double PromedioEstudiante(string estudianteId)
        {
            var e = BuscarEstudiante(estudianteId);
            var notas = _evaluaciones.Where(ev => ev.Estudiante.Id == e.Id).Select(ev => ev.Nota).ToList();
            if (notas.Count == 0) return 0;
            return Math.Round(notas.Average(), 2);
        }
    }
}
