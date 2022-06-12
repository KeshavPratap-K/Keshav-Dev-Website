using Keshav_Dev.Model;
using Microsoft.AspNetCore.Mvc;

namespace Keshav_Dev.Services;

public class ClipyClipboardCRUD: ControllerBase
{
    private readonly ClipyClipboardService _clipyClipboardService;

    public ClipyClipboardCRUD(ClipyClipboardService clipyClipboardService) =>
        _clipyClipboardService = clipyClipboardService;


    public async Task<List<ClipyClipboardFields>> ClipyClipboardGet() =>
    await _clipyClipboardService.GetAsync();

    public async Task<ActionResult<ClipyClipboardFields>> ClipyClipboardGet(string id)
    {
        var clipyClipboard = await _clipyClipboardService.GetAsync(id);

        if (clipyClipboard is null)
        {
            return NotFound();
        }

        return clipyClipboard;
    }


    public async Task<IActionResult> ClipyClipboardCreate(ClipyClipboardFields newClipyClipboardFields)
    {
        await _clipyClipboardService.CreateAsync(newClipyClipboardFields);

        return CreatedAtAction(nameof(ClipyClipboardGet), new { id = newClipyClipboardFields.IdShared }, newClipyClipboardFields);
    }


    public async Task<IActionResult> ClipyClipboardUpdate(string id, ClipyClipboardFields updatedClipyClipboardFields)
    {
        var clipyClipboardFields = await _clipyClipboardService.GetAsync(id);

        if (clipyClipboardFields is null)
        {
            return NotFound();
        }

        updatedClipyClipboardFields.IdShared = updatedClipyClipboardFields.IdShared;

        await _clipyClipboardService.UpdateAsync(id, updatedClipyClipboardFields);

        return NoContent();
    }


    public async Task<IActionResult> ClipyClipboardDelete(string id)
    {
        var clipyClipboardFields = await _clipyClipboardService.GetAsync(id);

        if (clipyClipboardFields is null)
        {
            return NotFound();
        }

        await _clipyClipboardService.RemoveAsync(id);

        return NoContent();
    }
}
