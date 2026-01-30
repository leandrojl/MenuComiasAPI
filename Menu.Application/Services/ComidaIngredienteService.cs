using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Application.Interfaces;
using Menu.Application.DTO.ComidaIngrediente;
using Menu.Domain.Entities;
using Menu.Domain.Interfaces;

namespace Menu.Application.Services
{
    public class ComidaIngredienteService : IComidaIngredienteService
    {
        private readonly IComidaIngredienteRepository _comidaIngredienteRepository;
        private readonly IComidaRepository _comidaRepository;
        private readonly IIngredienteRepository _ingredienteRepository;

        public ComidaIngredienteService(
            IComidaIngredienteRepository comidaIngredienteRepository,
            IComidaRepository comidaRepository,
            IIngredienteRepository ingredienteRepository)
        {
            _comidaIngredienteRepository = comidaIngredienteRepository;
            _comidaRepository = comidaRepository;
            _ingredienteRepository = ingredienteRepository;
        }

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS
        // ═══════════════════════════════════════════════════════════

        public async Task<IEnumerable<ComidaIngredienteDto>> GetAllAsync()
        {
            var relaciones = await _comidaIngredienteRepository.GetAllWithRelationsAsync();
            return relaciones.Select(MapToDto);
        }

        public async Task<ComidaIngredienteDto?> GetByIdAsync(int id)
        {
            var relacion = await _comidaIngredienteRepository.GetByIdWithRelationsAsync(id);
            
            if (relacion == null)
                return null;
            
            return MapToDto(relacion);
        }

        public async Task<IEnumerable<ComidaIngredienteDto>> GetByComidaIdAsync(int comidaId)
        {
            var relaciones = await _comidaIngredienteRepository.GetByComidaIdAsync(comidaId);
            return relaciones.Select(MapToDto);
        }

        public async Task<IEnumerable<ComidaIngredienteDto>> GetByIngredienteIdAsync(int ingredienteId)
        {
            var relaciones = await _comidaIngredienteRepository.GetByIngredienteIdAsync(ingredienteId);
            return relaciones.Select(MapToDto);
        }

        // ═══════════════════════════════════════════════════════════
        // ASIGNACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<ComidaIngredienteDto> AsignarIngredienteAsync(AsignarIngredienteDto dto)
        {
            // Validar que exista la comida
            var comidaExiste = await _comidaRepository.ExistsAsync(dto.ComidaId);
            if (!comidaExiste)
                throw new KeyNotFoundException($"Comida con ID {dto.ComidaId} no encontrada");
            
            // Validar que exista el ingrediente
            var ingredienteExiste = await _ingredienteRepository.ExistsAsync(dto.IngredienteId);
            if (!ingredienteExiste)
                throw new KeyNotFoundException($"Ingrediente con ID {dto.IngredienteId} no encontrado");
            
            // Validar que no exista ya la relación
            var existeRelacion = await _comidaIngredienteRepository.ExisteRelacionAsync(dto.ComidaId, dto.IngredienteId);
            if (existeRelacion)
                throw new InvalidOperationException($"El ingrediente ya está asignado a esta comida");
            
            var relacion = new ComidaIngrediente
            {
                ComidaId = dto.ComidaId,
                IngredienteId = dto.IngredienteId,
                Descripcion = dto.Descripcion
            };
            
            var relacionCreada = await _comidaIngredienteRepository.AddAsync(relacion);
            
            // Obtener la relación con sus navegaciones para el DTO
            var relacionConRelaciones = await _comidaIngredienteRepository.GetByIdWithRelationsAsync(relacionCreada.Id);
            
            return MapToDto(relacionConRelaciones!);
        }

        public async Task AsignarMultiplesIngredientesAsync(int comidaId, List<int> ingredienteIds)
        {
            // Validar que exista la comida
            var comidaExiste = await _comidaRepository.ExistsAsync(comidaId);
            if (!comidaExiste)
                throw new KeyNotFoundException($"Comida con ID {comidaId} no encontrada");
            
            foreach (var ingredienteId in ingredienteIds)
            {
                // Verificar que el ingrediente exista
                var ingredienteExiste = await _ingredienteRepository.ExistsAsync(ingredienteId);
                if (!ingredienteExiste)
                    continue; // Saltar ingredientes que no existen
                
                // Verificar que no exista ya la relación
                var existeRelacion = await _comidaIngredienteRepository.ExisteRelacionAsync(comidaId, ingredienteId);
                if (existeRelacion)
                    continue; // Saltar relaciones existentes
                
                var relacion = new ComidaIngrediente
                {
                    ComidaId = comidaId,
                    IngredienteId = ingredienteId
                };
                
                await _comidaIngredienteRepository.AddAsync(relacion);
            }
        }

        public async Task UpdateAsync(int id, UpdateComidaIngredienteDto dto)
        {
            var relacion = await _comidaIngredienteRepository.GetByIdAsync(id);
            
            if (relacion == null)
                throw new KeyNotFoundException($"Relación con ID {id} no encontrada");
            
            relacion.Descripcion = dto.Descripcion;
            
            await _comidaIngredienteRepository.UpdateAsync(relacion);
        }

        // ═══════════════════════════════════════════════════════════
        // DESASIGNACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task DeleteAsync(int id)
        {
            await _comidaIngredienteRepository.DeleteAsync(id);
        }

        public async Task DesasignarIngredienteAsync(int comidaId, int ingredienteId)
        {
            var relacion = await _comidaIngredienteRepository.GetByComidaAndIngredienteAsync(comidaId, ingredienteId);
            
            if (relacion != null)
            {
                await _comidaIngredienteRepository.DeleteAsync(relacion.Id);
            }
        }

        public async Task DesasignarTodosIngredientesAsync(int comidaId)
        {
            var relaciones = await _comidaIngredienteRepository.GetByComidaIdAsync(comidaId);
            
            foreach (var relacion in relaciones)
            {
                await _comidaIngredienteRepository.DeleteAsync(relacion.Id);
            }
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<bool> ExistsAsync(int id)
        {
            return await _comidaIngredienteRepository.ExistsAsync(id);
        }

        public async Task<bool> ExisteRelacionAsync(int comidaId, int ingredienteId)
        {
            return await _comidaIngredienteRepository.ExisteRelacionAsync(comidaId, ingredienteId);
        }

        // ═══════════════════════════════════════════════════════════
        // MAPEO PRIVADO
        // ═══════════════════════════════════════════════════════════

        private static ComidaIngredienteDto MapToDto(ComidaIngrediente ci)
        {
            return new ComidaIngredienteDto
            {
                Id = ci.Id,
                ComidaId = ci.ComidaId,
                ComidaNombre = ci.Comida?.Nombre ?? "",
                IngredienteId = ci.IngredienteId,
                IngredienteNombre = ci.Ingrediente?.Nombre ?? "",
                Descripcion = ci.Descripcion
            };
        }
    }
}
