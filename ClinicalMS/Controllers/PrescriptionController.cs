using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalMS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly ICreatePrescriptionService _createPrescriptionService;
        private readonly IPrescriptionQuery _prescriptionQuery;

        public PrescriptionController(
            ICreatePrescriptionService createPrescriptionService,
            IPrescriptionQuery prescriptionQuery)
        {
            _createPrescriptionService = createPrescriptionService;
            _prescriptionQuery = prescriptionQuery;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrescriptionResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { message = "El request no puede ser nulo" });
                }

                var prescription = await _createPrescriptionService.CreateAsync(request);
                return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.PrescriptionId }, prescription);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PrescriptionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            try
            {
                var prescription = await _prescriptionQuery.GetByIdAsync(id);
                if (prescription == null)
                {
                    return NotFound(new { message = "Receta no encontrada" });
                }

                var response = new PrescriptionResponse(
                    prescription.PrescriptionId,
                    prescription.PatientId,
                    prescription.DoctorId,
                    prescription.EncounterId,
                    prescription.Diagnosis,
                    prescription.Medication,
                    prescription.Dosage,
                    prescription.Frequency,
                    prescription.Duration,
                    prescription.AdditionalInstructions,
                    prescription.PrescriptionDate,
                    prescription.CreatedAt,
                    prescription.UpdatedAt
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<PrescriptionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionsByPatient(long patientId)
        {
            try
            {
                var prescriptions = await _prescriptionQuery.GetByPatientIdAsync(patientId);
                var response = prescriptions.Select(p => new PrescriptionResponse(
                    p.PrescriptionId,
                    p.PatientId,
                    p.DoctorId,
                    p.EncounterId,
                    p.Diagnosis,
                    p.Medication,
                    p.Dosage,
                    p.Frequency,
                    p.Duration,
                    p.AdditionalInstructions,
                    p.PrescriptionDate,
                    p.CreatedAt,
                    p.UpdatedAt
                ));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(typeof(IEnumerable<PrescriptionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionsByDoctor(long doctorId)
        {
            try
            {
                var prescriptions = await _prescriptionQuery.GetByDoctorIdAsync(doctorId);
                var response = prescriptions.Select(p => new PrescriptionResponse(
                    p.PrescriptionId,
                    p.PatientId,
                    p.DoctorId,
                    p.EncounterId,
                    p.Diagnosis,
                    p.Medication,
                    p.Dosage,
                    p.Frequency,
                    p.Duration,
                    p.AdditionalInstructions,
                    p.PrescriptionDate,
                    p.CreatedAt,
                    p.UpdatedAt
                ));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("encounter/{encounterId}")]
        [ProducesResponseType(typeof(IEnumerable<PrescriptionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionsByEncounter(int encounterId)
        {
            try
            {
                var prescriptions = await _prescriptionQuery.GetByEncounterIdAsync(encounterId);
                var response = prescriptions.Select(p => new PrescriptionResponse(
                    p.PrescriptionId,
                    p.PatientId,
                    p.DoctorId,
                    p.EncounterId,
                    p.Diagnosis,
                    p.Medication,
                    p.Dosage,
                    p.Frequency,
                    p.Duration,
                    p.AdditionalInstructions,
                    p.PrescriptionDate,
                    p.CreatedAt,
                    p.UpdatedAt
                ));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

