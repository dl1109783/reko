#region License
/* 
 * Copyright (C) 1999-2020 John Källén.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using NUnit.Framework;
using Reko.Arch.Mos6502;
using Reko.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UnitTests.Arch.Mos6502
{
    [TestFixture]
    public class EmulatorTests
    {
        private ServiceContainer sc;
        private Mos6502ProcessorArchitecture arch = new Mos6502ProcessorArchitecture("mos6502");
        private Mos6502Emulator emu;

        public EmulatorTests()
        {
            this.sc = new ServiceContainer();
        }

       

        private void Given_Code(Action<Assembler> p)
        {
            var asm = new Assembler(sc, new DefaultPlatform(sc, arch), Address.Ptr16(0x0800), new List<ImageSymbol>());
            p(asm);
            var program = asm.GetImage();
            program.SegmentMap.AddSegment(
                new MemoryArea(Address.Ptr16(0), new byte[256]),
                "ZeroPage",
                AccessMode.ReadWriteExecute);

            var envEmu = new DefaultPlatformEmulator();

            emu = (Mos6502Emulator) arch.CreateEmulator(program.SegmentMap, envEmu);
            emu.InstructionPointer = program.ImageMap.BaseAddress;
            emu.WriteRegister(Registers.s, 0xFF);
            emu.ExceptionRaised += (sender, e) => { throw e.Exception; };
        }

        [Test]
        public void Emu6502_ldy_imm()
        {
            Given_Code(m =>
            {
                m.Ldy(m.i8(0xC4)); // A0 C4 ldy #$C4
            });

            emu.Start();

            Assert.AreEqual(0xC4, emu.ReadRegister(Registers.y));
        }

        [Test]
        public void Emu6502_lda_abs_y()
        {
            Given_Code(m =>
            {
                m.Db(0x42);
                m.Ldy(m.i8(4));         // A0 04    ldy #$04
                m.Lda(m.ay(0x07FC));    // B9 3C 08 lda $083C,y
            });
            emu.InstructionPointer += 1;
            emu.Start();

            Assert.AreEqual(0x42, emu.ReadRegister(Registers.a));
        }

        [Test]
        public void Emu6502_ldx_zp()
        {
            Given_Code(m =>
            {
                m.Ldx(m.zp(4));         // A6 04    ldx $04
            });
            emu.WriteByte(0x04, 0x42);
            emu.Start();

            Assert.AreEqual(0x42, emu.ReadRegister(Registers.x));
        }

        [Test]
        public void Emu6502_sta_zpx()
        {
            Given_Code(m =>
            {
                m.Lda(m.i8(0x42));      // lda  #$42
                m.Ldx(m.i8(0x04));      // ldx  #$04
                m.Sta(m.zpX(0x04));     // sta  $04,x
            });
            emu.Start();
            emu.TryReadByte(0x0008, out var b);
            Assert.AreEqual(0x42, b);
        }
    }
    /* ﻿
    0818 
    0824 88 dey 
    0825 D0 F1 bne #$818
    0827 A0 09 ldy #$09
    082F 88 dey 
    0830 D0 F7 bne #$829
    0832 A9 51 lda #$51
    0834 85 2D sta $2D
    0836 A9 55 lda #$55
    0838 85 2E sta $2E
    083A 4C 00 01 jmp $0100
    ﻿0967 78 sei 
    0968 E6 01 inc $01
    096A 4C 16 08 jmp $0816
    */
}
