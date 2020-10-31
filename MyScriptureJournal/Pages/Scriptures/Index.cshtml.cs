using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;


namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public string BookSearchString { get; set; }
        public SelectList SortList { get; private set; }
        
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }
        private string sortOrder = "default";
        public SelectListItem bookListItem = new SelectListItem();
        public string EntryDateSort { get; set; }
        public string BookSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string SortOrder { get => sortOrder; set => sortOrder = value; }

        

        public async Task OnGetAsync(string sortOrder)
        {
            // using System to sort items
            BookSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            EntryDateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Scripture> scriptures1 = from s in _context.Scripture
                                                select s;

            switch (sortOrder)
            {
                case "name_desc":
                    scriptures1 = scriptures1.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    scriptures1 = scriptures1.OrderBy(s => s.EntryDate);
                    break;
                case "date_desc":
                    scriptures1 = scriptures1.OrderByDescending(s => s.EntryDate);
                    break;

                default:
                    scriptures1 = scriptures1.OrderBy(s => s.Book);
                    break;
            }

            Scripture = await scriptures1.ToListAsync();




            // using system.linq to get sorting

            IQueryable<string> bookQuery = from m in _context.Scripture
                                            orderby m.Book
                                            select m.Book;


            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures1 = scriptures1.Where(s => s.JournalEntry.Contains(SearchString));

            }
            if (!string.IsNullOrEmpty(ScriptureBook))
            {
                scriptures1 = scriptures1.Where(x => x.Book == ScriptureBook);
            }

            if (!string.IsNullOrEmpty(BookSearchString))
            {
                scriptures1 = scriptures1.Where(s => s.Book.Contains(BookSearchString));
            }

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Scripture = await scriptures1.AsNoTracking().ToListAsync();
        }
    }
}
