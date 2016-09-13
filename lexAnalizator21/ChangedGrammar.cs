using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    static class ChangedGrammar
    {
        private static List<StrChangedGrammar> changedGrammar = new List<StrChangedGrammar>()
        {
            new StrChangedGrammar("$programma$", "program enter var $obv$ label $sp_metok1$ enter begin $sp_oper1$ end"),
           // new StrChangedGrammar("$sp_obv1$", "$sp_obv$"),
           // new StrChangedGrammar("$sp_obv$", "$obv$ enter"),
          //  new StrChangedGrammar("$sp_obv$", "$sp_obv$ $obv1$ enter"),
            new StrChangedGrammar("$obv$", "$sp_id$ : real"),
            new StrChangedGrammar("$sp_id1$", "$sp_id$"),
            new StrChangedGrammar("$sp_id$", ", id"),
            new StrChangedGrammar("$sp_id$", "$sp_id$ , id"),
            new StrChangedGrammar("$sp_oper1$", "$sp_oper$"),
 
            new StrChangedGrammar("$sp_oper$", "$oper$ enter"),
            new StrChangedGrammar("$sp_oper$", "$sp_oper$ $oper$ enter"),
            new StrChangedGrammar("$oper$", "$nepom_oper1$"),
            new StrChangedGrammar("$oper$", "metka : $nepom_oper1$"),
            new StrChangedGrammar("$nepom_oper1$", "$nepom_oper$"),
            new StrChangedGrammar("$nepom_oper$", "read ( $sp_id1$ )"),
            new StrChangedGrammar("$nepom_oper$", "write ( $sp_id1$ )"),
            new StrChangedGrammar("$nepom_oper$", "id = $vyr1$"),
            new StrChangedGrammar("$nepom_oper$", "do id = $vyr1$ to $vyr1$ $sp_oper1$ next"),
            new StrChangedGrammar("$nepom_oper$", "if $lv1$ then goto metka"),
            new StrChangedGrammar("$lv1$", "$lv$"),
            new StrChangedGrammar("$lv$", "$lt1$"),
            new StrChangedGrammar("$lv$", "$lv$ or $lt1$"),
            new StrChangedGrammar("$lt1$", "$lt$"),
            new StrChangedGrammar("$lt$", "$lm$"),
            new StrChangedGrammar("$lt$", "$lt$ and $lm$"),
            new StrChangedGrammar("$lm$", "[ $lv1$ ]"),
            new StrChangedGrammar("$lm$", "$otnosh$"),
            new StrChangedGrammar("$lm$", "! $lm$"),
            new StrChangedGrammar("$otnosh$", "$vyr1$ $znak_otnosh$ $vyr1$"),
            new StrChangedGrammar("$znak_otnosh$", "<"),
            new StrChangedGrammar("$znak_otnosh$", ">"),
            new StrChangedGrammar("$znak_otnosh$", "<="),
            new StrChangedGrammar("$znak_otnosh$", ">="),
            new StrChangedGrammar("$znak_otnosh$", "=="),
            new StrChangedGrammar("$znak_otnosh$", "!="),
            new StrChangedGrammar("$sp_metok1$", "$sp_metok$"),
            new StrChangedGrammar("$sp_metok$", "metka"),
            new StrChangedGrammar("$sp_metok$", "$sp_metok$ , metka"),
            new StrChangedGrammar("$vyr1$", "$vyr$"),
            new StrChangedGrammar("$vyr$", "$term1$"),
            new StrChangedGrammar("$vyr$", "$vyr$ + $term1$"),
            new StrChangedGrammar("$vyr$", "$vyr$ - $term1$"),
            new StrChangedGrammar("$term1$", "$term$"),
            new StrChangedGrammar("$term$", "$mnozh1$"),
            new StrChangedGrammar("$term$", "$term$ * $mnozh1$"),
            new StrChangedGrammar("$term$", "$term$ / $mnozh1$"),
            new StrChangedGrammar("$mnozh1$", "$mnozh$"),
            new StrChangedGrammar("$mnozh$", "id"),
            new StrChangedGrammar("$mnozh$", "cons"),
            new StrChangedGrammar("$mnozh$", "( $vyr1$ )"),
            new StrChangedGrammar("$enter1$", "enter")
        };

        public static List<StrChangedGrammar> GetChangedGrammar() 
        {
            return changedGrammar;

        }
    }
}
