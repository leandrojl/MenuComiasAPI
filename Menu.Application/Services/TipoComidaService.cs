using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Application.Interfaces;
using Menu.Application.DTO.TipoComida;
using Menu.Application.DTO.Comida;
using Menu.Domain.Entities;
using Menu.Domain.Interfaces;

namespace Menu.Application.Services
{
    public class TipoComidaService : ITipoComidaService
    {
        private readonly ITipoComidaRepository _tipoComidaRepository;

        public TipoComidaService(ITipoComidaRepository tipoComidaRepository)
        {
            _tipoComidaRepository = tipoComidaRepository;
        }

        // ═══════════════════════════════════════════════════════════
        // OPERACIONES CRUD BÁSICAS
        // ═══════════════════════════════════════════════════════════

        public async Task<IEnumerable<TipoComidaDto>> GetAllAsync()
        {
            var tiposComida = await _tipoComidaRepository.GetAllAsync();
            
            return tiposComida.Select(t => new TipoComidaDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                CantidadComidas = t.Comidas?.Count ?? 0
            });
        }

        public async Task<TipoComidaDto?> GetByIdAsync(int id)
        {
            var tipoComida = await _tipoComidaRepository.GetByIdAsync(id);
            
            if (tipoComida == null)
                return null;
            
            return new TipoComidaDto
            {
                Id = tipoComida.Id,
                Nombre = tipoComida.Nombre,
                CantidadComidas = await _tipoComidaRepository.ContarComidasAsync(id)
            };
        }

        public async Task<TipoComidaDto> CreateAsync(CreateTipoComidaDto dto)
        {
            // Validar que no exista el nombre
            var existeNombre = await _tipoComidaRepository.ExisteNombreAsync(dto.Nombre);
            if (existeNombre)
                throw new InvalidOperationException($"Ya existe un tipo de comida con el nombre '{dto.Nombre}'");
            
            var tipoComida = new TipoComida
            {
                Nombre = dto.Nombre
            };
            
            var tipoComidaCreado = await _tipoComidaRepository.AddAsync(tipoComida);
            
            return new TipoComidaDto
            {
                Id = tipoComidaCreado.Id,
                Nombre = tipoComidaCreado.Nombre,
                CantidadComidas = 0
            };
        }

        public async Task UpdateAsync(int id, UpdateTipoComidaDto dto)
        {
            var tipoComida = await _tipoComidaRepository.GetByIdAsync(id);
            
            if (tipoComida == null)
                throw new KeyNotFoundException($"Tipo de comida con ID {id} no encontrado");
            
            // Validar que no exista otro con el mismo nombre
            var existeNombre = await _tipoComidaRepository.ExisteNombreAsync(dto.Nombre, id);
            if (existeNombre)
                throw new InvalidOperationException($"Ya existe otro tipo de comida con el nombre '{dto.Nombre}'");
            
            tipoComida.Nombre = dto.Nombre;
            
            await _tipoComidaRepository.UpdateAsync(tipoComida);
        }

        public async Task DeleteAsync(int id)
        {
            await _tipoComidaRepository.DeleteAsync(id);
        }

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS CON RELACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<TipoComidaWithComidasDto?> GetByIdWithComidasAsync(int id)
        {
            var tipoComida = await _tipoComidaRepository.GetByIdWithComidasAsync(id);
            
            if (tipoComida == null)
                return null;
            
            return MapToTipoComidaWithComidasDto(tipoComida);
        }

        public async Task<IEnumerable<TipoComidaWithComidasDto>> GetAllWithComidasAsync()
        {
            var tiposComida = await _tipoComidaRepository.GetAllWithComidasAsync();
            
            return tiposComida.Select(MapToTipoComidaWithComidasDto);
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<bool> ExistsAsync(int id)
        {
            return await _tipoComidaRepository.ExistsAsync(id);
        }

        public async Task<bool> TieneComidasAsync(int id)
        {
            return await _tipoComidaRepository.TieneComidasAsync(id);
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _tipoComidaRepository.ExisteNombreAsync(nombre);
        }

        // ═══════════════════════════════════════════════════════════
        // MÉTODOS PRIVADOS DE MAPEO
        // ═══════════════════════════════════════════════════════════

        private static TipoComidaWithComidasDto MapToTipoComidaWithComidasDto(TipoComida tipoComida)
        {
            return new TipoComidaWithComidasDto
            {
                Id = tipoComida.Id,
                Nombre = tipoComida.Nombre,
                CantidadComidas = tipoComida.Comidas?.Count ?? 0,
                Comidas = tipoComida.Comidas?.Select(c => new ComidaSimpleDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Precio = c.Precio,
                    Porcion = c.Porcion,
                    CuantasPersonasComen = c.CuantasPersonasComen
                }).ToList() ?? new List<ComidaSimpleDto>()
            };
        }
    }
}
