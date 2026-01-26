using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;
using Menu.Domain.Interfaces;
using Menu.Application.Interfaces;
using Menu.Application.DTO.Comida;
using Menu.Application.DTO.Ingrediente;

namespace Menu.Application.Services
{
    public class ComidaService : IComidaService
    {
        
        private readonly IComidaRepository _comidaRepository;
        private readonly ITipoComidaRepository _tipoComidaRepository;

        public ComidaService(IComidaRepository comidaRepository, ITipoComidaRepository tipoComidaRepository)
        {
            _comidaRepository = comidaRepository;
            _tipoComidaRepository = tipoComidaRepository;
        }

        public async Task<IEnumerable<ComidaDto>> GetAllAsync()
        {
            // 1. Obtener entidades del repositorio
            var comidas = await _comidaRepository.GetAllAsync();
            
            // 2. Mapear cada entidad a DTO
            return comidas.Select(c => new ComidaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Precio = c.Precio,
                Porcion = c.Porcion,
                CuantasPersonasComen = c.CuantasPersonasComen,
                TipoComidaId = c.TipoComidaId,
                TipoComidaNombre = c.TipoComida?.Nombre ?? "",
                Ingredientes = new List<IngredienteDto>() // Por ahora vacío
            });
        }

        public async Task<ComidaDto?> GetByIdAsync(int id)
        {
            var comida = await _comidaRepository.GetByIdAsync(id);
            
            if (comida == null)
                return null;
            
            return new ComidaDto
            {
                Id = comida.Id,
                Nombre = comida.Nombre,
                Precio = comida.Precio,
                Porcion = comida.Porcion,
                CuantasPersonasComen = comida.CuantasPersonasComen,
                TipoComidaId = comida.TipoComidaId,
                TipoComidaNombre = comida.TipoComida?.Nombre ?? "",
                Ingredientes = new List<IngredienteDto>()
            };
        }

        public async Task<ComidaDto> CreateAsync(CreateComidaDto dto)
        {
            // 1. Convertir DTO a Entidad
            var comida = new Comida
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Porcion = dto.Porcion,
                CuantasPersonasComen = dto.CuantasPersonasComen,
                TipoComidaId = dto.TipoComidaId
            };
            
            // 2. Guardar en base de datos
            var comidaCreada = await _comidaRepository.AddAsync(comida);
            
            // 3. Retornar como DTO
            return new ComidaDto
            {
                Id = comidaCreada.Id,
                Nombre = comidaCreada.Nombre,
                Precio = comidaCreada.Precio,
                Porcion = comidaCreada.Porcion,
                CuantasPersonasComen = comidaCreada.CuantasPersonasComen,
                TipoComidaId = comidaCreada.TipoComidaId,
                TipoComidaNombre = "",
                Ingredientes = new List<IngredienteDto>()
            };
        }

        public async Task UpdateAsync(int id, UpdateComidaDto dto)
        {
            // 1. Buscar la comida existente
            var comida = await _comidaRepository.GetByIdAsync(id);
            
            if (comida == null)
                throw new KeyNotFoundException($"Comida con ID {id} no encontrada");
            
            // 2. Actualizar propiedades
            comida.Nombre = dto.Nombre;
            comida.Precio = dto.Precio;
            comida.Porcion = dto.Porcion;
            comida.CuantasPersonasComen = dto.CuantasPersonasComen;
            comida.TipoComidaId = dto.TipoComidaId;
            
            // 3. Guardar cambios
            await _comidaRepository.UpdateAsync(comida);
        }

        public async Task DeleteAsync(int id)
        {
            await _comidaRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _comidaRepository.ExistsAsync(id);
        }
                
    
    }
}
