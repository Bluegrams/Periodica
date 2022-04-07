using System;
using Windows.ApplicationModel.Resources;

namespace Bluegrams.Periodica.Data
{
    public static class ImageResourceTables
    {
        private static ResourceLoader loader = ResourceLoader.GetForViewIndependentUse("ImageResources");

        // Some common sources
        static string photoBy = loader.GetString("strPhotoBy");
        static string lic_IOE = $"[{photoBy} images-of-elements.com, CC BY 3.0]";
        static string lic_AHP_FAL = $"[{photoBy} Alchemist-hp (www.pse-mendelejew.de), FAL]";
        static string lic_AHP_GFDL = $"[{photoBy} Alchemist-hp (www.pse-mendelejew.de), GFDL 1.2]";
        static string lic_AHP_CC = $"[{photoBy} Alchemist-hp (www.pse-mendelejew.de), CC BY-SA 3.0]";
        static string lic_THD = $"[{photoBy} Tomihahndorf, Public Domain]";
        static string lic_WAR = $"[{photoBy} Warut Roonguthai, CC BY-SA 3.0]";

        public static ImageResource[][] Images = new ImageResource[][]
        {
            //H
            new ImageResource[]
            {
                new ImageResource("h_orion.jpg", "[NASA/ ESA]", null) { Description=loader.GetString("Himg1") },
                new ImageResource("h.jpg", lic_IOE,
                    "http://images-of-elements.com/hydrogen.php")
            },
            //He
            new ImageResource[]
            {
                new ImageResource("he_sun.jpg", "[NASA]", null) { Description=loader.GetString("HEimg1") },
                new ImageResource("he.jpg", lic_IOE,
                    "http://images-of-elements.com/helium.php")
            },
            //Li
            new ImageResource[]
            {
                new ImageResource("li.jpg", lic_THD,
                    "https://commons.wikimedia.org/wiki/File:Lithium_paraffin.jpg") { Description=loader.GetString("LIimg1") },
                new ImageResource("li_black.jpg", lic_IOE,
                    "http://images-of-elements.com/lithium.php")
            },
            //Be
            new ImageResource[]
            {
                new ImageResource("be.jpg", lic_IOE,
                    "http://images-of-elements.com/beryllium.php")
            },
            //B
            new ImageResource[]
            {
                new ImageResource("b.jpg", lic_IOE,
                    "http://images-of-elements.com/boron.php")
            },
            //C
            new ImageResource[]
            {
                new ImageResource("c_coal.jpg", "", null),
                new ImageResource("c.jpg", $"[{photoBy} Rob Lavinsky, iRocks.com, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=12429985")
            },
            // N
            new ImageResource[]
            {
                new ImageResource("n_tube.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10942390")
            },
            //O
            new ImageResource[]
            {
                new ImageResource("o_bubbles.jpg", "", null) { Description=loader.GetString("Oimg1") },
                new ImageResource("o_northern.jpg", null, null) { Description=loader.GetString("Oimg2") }
            },
            //F
            new ImageResource[]
            {
                new ImageResource("f.jpg", $"[{photoBy} Prof B. G. Mueller, derivative work: TCO, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=15472526")
            },
            //Ne
            new ImageResource[]
            {
                new ImageResource("ne_tube.jpg", lic_AHP_GFDL,
                    "https://commons.wikimedia.org/w/index.php?curid=9597853"),
                new ImageResource("ne.jpg", lic_IOE,
                    "http://images-of-elements.com/neon.php")
            },
            //Na
            new ImageResource[]
            {
                new ImageResource("na.jpg", $"[{photoBy} Dnn87 at English Wikipedia, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=3831512")
            },
            //Mg
            new ImageResource[]
            {
                new ImageResource("mg_sheets.jpg", $"[{photoBy} CSIRO, CC BY 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=35482148") { Description=loader.GetString("MGimg1") },
                new ImageResource("mg.jpg", lic_WAR,
                    "https://commons.wikimedia.org/w/index.php?curid=3017081")
            },
            //Al
            new ImageResource[]
            {
                new ImageResource("al_surface.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=9531022")
            },
            //Si
            new ImageResource[]
            {
                new ImageResource("si.jpg", lic_IOE,
                    "http://images-of-elements.com/silicon.php"),
                new ImageResource("si_rod.jpg", lic_WAR,
                    "https://commons.wikimedia.org/w/index.php?curid=2945347")
            },
            //P
            new ImageResource[]
            {
                new ImageResource("p.jpg", lic_IOE,
                    "http://images-of-elements.com/phosphorus.php")
            },
            //S
            new ImageResource[]
            {
                new ImageResource("s.jpg", "", null)
            },
            //Cl
            new ImageResource[]
            {
                new ImageResource("cl.jpg", lic_IOE,
                    "http://images-of-elements.com/chlorine.php")
            },
            //Ar
            new ImageResource[]
            {
                new ImageResource("ar_tube.jpg", lic_AHP_GFDL,
                    "https://commons.wikimedia.org/w/index.php?curid=9597870"),
                new ImageResource("ar.jpg", lic_IOE, "http://images-of-elements.com/argon.php")
            },
            //K
            new ImageResource[]
            {
                new ImageResource("k.jpg", $"[{photoBy} Dnn87, CC BY 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=3213111")
            },
            //Ca
            new ImageResource[]
            {
                new ImageResource("ca.jpg", "", null)
            },
            //Sc
            new ImageResource[]
            {
                new ImageResource("sc.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10636841")
            },
            //Ti
            new ImageResource[]
            {
                new ImageResource("ti.jpg", lic_AHP_CC,
                    "https://commons.wikimedia.org/wiki/File:Titan-crystal_bar.JPG")
            },
            //V
            new ImageResource[]
            {
                new ImageResource("v_bar.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=14948517")
            },
            //Cr
            new ImageResource[]
            {
                new ImageResource("cr.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10125753")
            },
            //Mn
            new ImageResource[]
            {
                new ImageResource("mn.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=11930318")
            },
            //Fe
            new ImageResource[]
            {
                new ImageResource("fe_rods.jpg", null, null) { Description=loader.GetString("FEimg1") },
                new ImageResource("fe.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10115787")
            },
            //Co
            new ImageResource[]
            {
                new ImageResource("co.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=11530303")
            },
            //Ni
            new ImageResource[]
            {
                new ImageResource("ni.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=11536245")
            },
            //Cu
            new ImageResource[]
            {
                new ImageResource("cu_euro.jpg", "", null),
                new ImageResource("cu.jpg", $"[{photoBy} Jonathan Zander, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=7223304")
            },
            //Zn
            new ImageResource[]
            {
                new ImageResource("zn.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=11660410")
            },
            //Ga
            new ImageResource[]
            {
                new ImageResource("ga.jpg", $"[{photoBy} user:foobar, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=12703")
            },
            //Ge
            new ImageResource[]
            {
                new ImageResource("ge.jpg", lic_IOE,
                    "http://images-of-elements.com/germanium.php")
            },
            //As
            new ImageResource[]
            {
                new ImageResource("as.jpg", lic_IOE,
                    "http://images-of-elements.com/arsenic.php")
            },
            //Se
            new ImageResource[]
            {
                new ImageResource("se.jpg", $"[{photoBy} W. Oelen, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/wiki/File:SeBlackRed.jpg") { Description=loader.GetString("SEimg1") }
            },
            //Br
            new ImageResource[]
            {
                new ImageResource("br.jpg", lic_IOE,
                    "http://images-of-elements.com/bromine.php")
            },
            //Kr
            new ImageResource[]
            {
                new ImageResource("kr_tube.jpg", lic_AHP_GFDL,
                    "https://commons.wikimedia.org/w/index.php?curid=9597887")
            },
            //Rb
            new ImageResource[]
            {
                new ImageResource("rb.jpg", $"[{photoBy} Dnn87, CC BY 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=3254245")
            },
            //Sr
            new ImageResource[]
            {
                new ImageResource("sr.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=14666467"),
                new ImageResource("sr_fireworks.jpg", null, null) { Description=loader.GetString("SRimg2") }
            },
            //Y
            new ImageResource[]
            {
                new ImageResource("y.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=9462967")
            },
            //Zr
            new ImageResource[]
            {
                new ImageResource("zr_bar.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=14861211")
            },
            //Nb
            new ImageResource[]
            {
                new ImageResource("nb_crystals.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10489915")
            },
            //Mo
            new ImageResource[]
            {
                new ImageResource("mo.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10399404")
            },
            //Tc
            new ImageResource[]
            {
                new ImageResource("tc.jpg", "", null)
            },
            //Ru
            new ImageResource[]
            {
                new ImageResource("ru.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=9915539")
            },
            //Rh
            new ImageResource[]
            {
                new ImageResource("rh_powder.jpg", lic_AHP_CC,
                    "https://commons.wikimedia.org/w/index.php?curid=7636785")
            },
            //Pd
            new ImageResource[]
            {
                new ImageResource("pd.jpg", lic_IOE,
                    "http://images-of-elements.com/palladium.php")
            },
            //Ag
            new ImageResource[]
            {
                new ImageResource("ag_crystal.jpg", lic_AHP_CC,
                    "https://commons.wikimedia.org/w/index.php?curid=7394995")
            },
            //Cd
            new ImageResource[]
            {
                new ImageResource("cd.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10323830")
            },
            //In
            new ImageResource[]
            {
                new ImageResource("in.jpg", lic_IOE, "http://images-of-elements.com/indium.php")
            },
            //Sn
            new ImageResource[]
            {
                new ImageResource("sn.jpg", lic_IOE,
                    "http://images-of-elements.com/pse/zinn.php")
            },
            //Sb
            new ImageResource[]
            {
                new ImageResource("sb.jpg", lic_IOE, "http://images-of-elements.com/antimony.php")
            },
            //Te
            new ImageResource[]
            {
                new ImageResource("te.jpg", lic_IOE, "http://images-of-elements.com/tellurium.php")
            },
            //I
            new ImageResource[]
            {
                new ImageResource("i.jpg", lic_IOE, "http://images-of-elements.com/iodine.php"),
                new ImageResource("i_crystal.jpg", lic_THD, null)
            },
            //Xe
            new ImageResource[]
            {
                new ImageResource("xe_tube.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=9598160")
            },
            //Cs
            new ImageResource[]
            {
                new ImageResource("cs.jpg", $"[{photoBy} Dnn87 - Transferred from en.wikipedia, CC BY-SA 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=3691519")
            },
            //Ba
            new ImageResource[]
            {
                new ImageResource("ba_fireworks.jpg", null, null) { Description=loader.GetString("BAimg1") }
            },
            //La
            new ImageResource[]
            {
                new ImageResource("la.jpg", lic_IOE,
                    "http://images-of-elements.com/lanthanum.php")
            },
            //Ce
            new ImageResource[]
            {
                new ImageResource("ce.jpg", lic_IOE,
                    "http://images-of-elements.com/pse/cer.php")
            },
            //Pr
            new ImageResource[]
            {
                new ImageResource("pr.jpg", lic_IOE,
                    "http://images-of-elements.com/praseodymium.php")
            },
            //Nd
            new ImageResource[]
            {
                new ImageResource("nd.jpg", lic_IOE,
                    "http://images-of-elements.com/neodymium.php")
            },
            //Pm
            new ImageResource[]
            {

            },
            //Sm
            new ImageResource[]
            {
                new ImageResource("sm.jpg", lic_IOE,
                    "http://images-of-elements.com/samarium.php")
            },
            //Eu
            new ImageResource[]
            {
                new ImageResource("eu_block.jpg", lic_AHP_CC,
                    "https://commons.wikimedia.org/w/index.php?curid=7257290")
            },
            //Gd
            new ImageResource[]
            {
                new ImageResource("gd.jpg", lic_IOE,
                    "http://images-of-elements.com/gadolinium.php")
            },
            //Tb
            new ImageResource[]
            {
                new ImageResource("tb.jpg", lic_IOE,
                    "http://images-of-elements.com/terbium.php")
            },
            //Dy
            new ImageResource[]
            {
                new ImageResource("dy.jpg", lic_IOE,
                    "http://images-of-elements.com/dysprosium.php")
            },
            //Ho
            new ImageResource[]
            {
                new ImageResource("ho.jpg", lic_IOE,
                    "http://images-of-elements.com/holmium.php")
            },
            //Er
            new ImageResource[]
            {
                new ImageResource("er.jpg", lic_IOE,
                    "http://images-of-elements.com/erbium.php")
            },
            //Tm
            new ImageResource[]
            {
                new ImageResource("tm.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=12227264")
            },
            //Yb
            new ImageResource[]
            {
                new ImageResource("yb.jpg", lic_IOE,
                    "http://images-of-elements.com/ytterbium.php")
            },
            //Lu
            new ImageResource[]
            {
                new ImageResource("lu.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=12218007")
            },
            //Hf
            new ImageResource[]
            {
                new ImageResource("hf.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=14868225")
            },
            //Ta
            new ImageResource[]
            {
                new ImageResource("ta.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10489977")
            },
            //W
            new ImageResource[]
            {
                new ImageResource("w.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=10424635")
            },
            //Re
            new ImageResource[]
            {
                new ImageResource("re.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=11594094")
            },
            //Os
            new ImageResource[]
            {
                new ImageResource("os.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=9495965")
            },
            //Ir
            new ImageResource[]
            {
                new ImageResource("ir.jpg", lic_IOE,
                    "http://images-of-elements.com/iridium.php")
            },
            //Pt
            new ImageResource[]
            {
                new ImageResource("pt.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=9579015")
            },
            //Au
            new ImageResource[]
            {
                new ImageResource("au_bar.jpg", "", null)
            },
            //Hg
            new ImageResource[]
            {
                new ImageResource("hg.jpg", lic_IOE,
                    "http://images-of-elements.com/mercury.php")
            },
            //Tl
            new ImageResource[]
            {
                new ImageResource("tl.jpg", lic_IOE, "http://images-of-elements.com/thallium.php")
            },
            //Pb
            new ImageResource[]
            {
                new ImageResource("pb.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=12381318")
            },
            //Bi
            new ImageResource[]
            {
                new ImageResource("bi_crystal.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=12387324")
            },
            //Po
            new ImageResource[]
            {

            },
            //At
            new ImageResource[]
            {

            },
            //Rn
            new ImageResource[]
            {

            },
            //Fr
            new ImageResource[]
            {

            },
            //Ra
            new ImageResource[]
            {
                new ImageResource("ra_clock.jpg", lic_IOE,
                    "http://images-of-elements.com/radium.php") { Description=loader.GetString("RAimg1") }
            },
            //Ac
            new ImageResource[]
            {
                
            },
            //Th
            new ImageResource[]
            {
                new ImageResource("th_sample.jpg", lic_AHP_FAL,
                    "https://commons.wikimedia.org/w/index.php?curid=15037460")
            },
            //Pa
            new ImageResource[]
            {
                
            },
            //U
            new ImageResource[]
            {
                new ImageResource("u.jpg", "", null)
            },
            //Np
            new ImageResource[]
            {
                new ImageResource("np.jpg", null, null) { Description=loader.GetString("NPimg1") }
            },
            //Pu
            new ImageResource[]
            {
                new ImageResource("pu_bomb.jpg", null, null) { Description=loader.GetString("PUimg1") },
                new ImageResource("pu.jpg", "", null)
            },
            //Am
            new ImageResource[]
            {
                new ImageResource("am.jpg", $"[{photoBy} Bionerd, CC BY 3.0]",
                    "https://commons.wikimedia.org/w/index.php?curid=11318926")
            },
            //Cm
            new ImageResource[]
            {
                
            },
            //Bk
            new ImageResource[]
            {
                new ImageResource("bk.jpg", "", null)
            },
            //Cf
            new ImageResource[]
            {
                
            },
            //Es
            new ImageResource[]
            {
                new ImageResource("es.jpg", "", null)
            },
            //Fm
            new ImageResource[]
            {
                new ImageResource("fm_bomb.jpg", null, null) { Description=loader.GetString("FMimg1") }
            },
            //Md
            new ImageResource[]
            {
                
            },
            //No
            new ImageResource[]
            {
                
            },
            //Lr
            new ImageResource[]
            {
                
            },
            //Rf
            new ImageResource[]
            {
                
            },
            //Db
            new ImageResource[]
            {
                
            },
            //Sg
            new ImageResource[]
            {
                
            },
            //Bh
            new ImageResource[]
            {
                
            },
            //Hs
            new ImageResource[]
            {
                
            },
            //Mt
            new ImageResource[]
            {
                
            },
            //Ds
            new ImageResource[]
            {
                
            },
            //Rg
            new ImageResource[]
            {
                
            },
            //Cn
            new ImageResource[]
            {
                
            },
            //Nh
            new ImageResource[]
            {
                
            },
            //Fl
            new ImageResource[]
            {
                
            },
            //Mc
            new ImageResource[]
            {
                
            },
            //Lv
            new ImageResource[]
            {
                
            },
            //Ts
            new ImageResource[]
            {
                
            },
            //Og
            new ImageResource[]
            {
                
            }
        };
    }
}
