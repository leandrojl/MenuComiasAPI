using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Application.Interfaces;
using Menu.Application.DTO.Ingrediente;
using Menu.Application.DTO.Comida;
using Menu.Domain.Entities;
using Menu.Domain.Interfaces;

namespace Menu.Application.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteRepository _ingredienteRepository;

        public IngredienteService(IIngredienteRepository ingredienteRepository)
        {
            _ingredienteRepository = ingredienteRepository;
        }

        // ═══════════════════════════════════════════════════════════
        // OPERACIONES CRUD BÁSICAS
        // ═══════════════════════════════════════════════════════════

        public async Task<IEnumerable<IngredienteDto>> GetAllAsync()
        {
            var ingredientes = await _ingredienteRepository.GetAllAsync();
            
            var result = new List<IngredienteDto>();
            foreach (var i in ingredientes)
            {
                result.Add(new IngredienteDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CantidadComidas = await _ingredienteRepository.ContarComidasAsync(i.Id)
                });
            }
            return result;
        }

        public async Task<IngredienteDto?> GetByIdAsync(int id)
        {
            var ingrediente = await _ingredienteRepository.GetByIdAsync(id);
            
            if (ingrediente == null)
                return null;
            
            return new IngredienteDto
            {
                Id = ingrediente.Id,
                Nombre = ingrediente.Nombre,
                CantidadComidas = await _ingredienteRepository.ContarComidasAsync(id)
            };
        }

        public async Task<IngredienteDto> CreateAsync(CreateIngredienteDto dto)
        {
            // Validar que no exista el nombre
            var existeNombre = await _ingredienteRepository.ExisteNombreAsync(dto.Nombre);
            if (existeNombre)
                throw new InvalidOperationException($"Ya existe un ingrediente con el nombre '{dto.Nombre}'");
            
            var ingrediente = new Ingrediente
            {
                Nombre = dto.Nombre
            };
            
            var ingredienteCreado = await _ingredienteRepository.AddAsync(ingrediente);
            
            return new IngredienteDto
            {
                Id = ingredienteCreado.Id,
                Nombre = ingredienteCreado.Nombre,
                CantidadComidas = 0
            };
        }

        public async Task UpdateAsync(int id, UpdateIngredienteDto dto)
        {
            var ingrediente = await _ingredienteRepository.GetByIdAsync(id);
            
            if (ingrediente == null)
                throw new KeyNotFoundException($"Ingrediente con ID {id} no encontrado");
            
            // Validar que no exista otro con el mismo nombre
            var existeNombre = await _ingredienteRepository.ExisteNombreAsync(dto.Nombre, id);
            if (existeNombre)
                throw new InvalidOperationException($"Ya existe otro ingrediente con el nombre '{dto.Nombre}'");
            
            ingrediente.Nombre = dto.Nombre;
            
            await _ingredienteRepository.UpdateAsync(ingrediente);
        }

        public async Task DeleteAsync(int id)
        {
            await _ingredienteRepository.DeleteAsync(id);
        }

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS CON RELACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<IngredienteWithComidasDto?> GetByIdWithComidasAsync(int id)
        {
            var ingrediente = await _ingredienteRepository.GetByIdWithComidasAsync(id);
            
            if (ingrediente == null)
                return null;
            
            return new IngredienteWithComidasDto
            {
                Id = ingrediente.Id,
                Nombre = ingrediente.Nombre,
                CantidadComidas = ingrediente.ComidaIngredientes?.Count ?? 0,
                Comidas = ingrediente.ComidaIngredientes?.Select(ci => new ComidaSimpleDto
                {
                    Id = ci.Comida.Id,
                    Nombre = ci.Comida.Nombre,
                    Precio = ci.Comida.Precio,
                    Porcion = ci.Comida.Porcion,
                    CuantasPersonasComen = ci.Comida.CuantasPersonasComen
                }).ToList() ?? new List<ComidaSimpleDto>()
            };
        }

        public async Task<IEnumerable<IngredienteDto>> SearchByNombreAsync(string nombre)
        {
            var ingredientes = await _ingredienteRepository.SearchByNombreAsync(nombre);
            
            return ingredientes.Select(i => new IngredienteDto
            {
                Id = i.Id,
                Nombre = i.Nombre,
                CantidadComidas = i.ComidaIngredientes?.Count ?? 0
            });
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<bool> ExistsAsync(int id)
        {
            return await _ingredienteRepository.ExistsAsync(id);
        }

        public async Task<bool> TieneComidasAsync(int id)
        {
            return await _ingredienteRepository.TieneComidasAsync(id);
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _ingredienteRepository.ExisteNombreAsync(nombre);
        }
    }
}
