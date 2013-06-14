﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decompiler.Arch.Sparc
{
    public enum Opcode
    {
        illegal = 0,

        and,
        andcc,
        andn,
        andncc,
        add,
        addcc,
        addx,
        addxcc,
        call,
        lda,
        ldc,
        ldcsr,
        ldda,
        lddc,
        lddf,
        ldfsr,
        ldf,
        ldstub,
        ldstuba,
        ldsh,
        ldsha,
        ldsb,
        ldsba,
        lduba,
        lduha,

        or,
        orcc,
        orn,
        orncc,
        sdiv,
        sdivcc,
        sethi,
        smul,
        smulcc,
        stc,
        stcsr,
        stdc,
        stdcq,
        stdf,
        stdfq,
        stf,
        stfsr,
        sub,
        subcc,
        subx,
        subxcc,
        swap,
        swapa,
        udiv,
        udivcc,
        umul,
        umulcc,
        unimp,
        xnor,
        xnorcc,
        xor,
        xorcc,
        stda,
        stha,
        stba,
        sta,
        std,
        sth,
        stb,
        st,
        ldd,
        lduh,
        ldub,
        ld,
        restore,
        save,
        flush,
        Ticc,
        rett,
        jmpl,
        wrtbr,
        wrwim,
        wrpsr,
        wrasr,
        rdtbr,
        rdpsr,
        sra,
        srl,
        sll,
        mulscc,
        tsubcctv,
        tsubcc,
        taddcctv,
        taddcc,
        fmovs,
        fnegs,
        tvc,
        tpos,
        tcc,
        tgu,
        tge,
        tg,
        tne,
        ta,
        tvs,
        tneg,
        tcs,
        tleu,
        tl,
        tle,
        te,
        tn,
        cb012,
        cb013,
        cb01,
        cb023,
        bn,
        be,
        bvc,
        bpos,
        bcc,
        bgu,
        bge,
        bg,
        bne,
        ba,
        bvs,
        bneg,
        bcs,
        bleu,
        bl,
        ble,
        fbug,
        fbu,
        fbg,
        fbne,
        fbn,
        fbul,
        fblg,
        cb2,
        cb23,
        cb1,
        cb13,
        cb12,
        cbn,
        fbo,
        fbule,
        fble,
        fbuge,
        fbge,
        cb03,
        cb3,
        cb123,
        cb02,
        cb0,
        cba,
        fbue,
        fbe,
        fba,
    }
}
