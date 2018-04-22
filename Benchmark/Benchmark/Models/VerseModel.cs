/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      VerseModel.cs
 * @revision:  1
 * @synapsis:  This is the model used for verses being searched for and inserted.
 *             .Net's built in data validation is used here to filter the user's
 *             input and return feedback.
 */

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Benchmark.Models
{
    public class VerseModel
    {
        [Required]
        [DisplayName("Testament")]
        [DefaultValue("")]
        public string Testament { get; set; }

        [Required]
        [DisplayName("Book")]
        [DefaultValue("")]
        public string Book { get; set; }

        [Required]
        [DisplayName("Chapter")]
        [DefaultValue("")]
        public int Chapter { get; set; }

        [Required]
        [DisplayName("Verse")]
        [DefaultValue("")]
        public int Verse { get; set; }

        //[Required] was removed here and placed in the view because this causes the search to fail
        [DisplayName("Text")]
        [StringLength(150, MinimumLength = 5)]
        [DefaultValue("")]
        public string Text { get; set; }
    }
}