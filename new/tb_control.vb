`timescale 1ns / 1ps

module tb_control( );
    reg [6:0] opcode;
    reg [2:0] funct3;
    reg [6:0] funct7;
    wire branch, mem_read, mem_to_reg, mem_write, ALU_src, reg_write;
    wire [3:0] ALU_control, write_mask;
        
    control uut(
        .opcode(opcode),
        .funct3(funct3),
        .funct7(funct7),
        .branch(branch),
        .write_mask(write_mask),
        .mem_read(mem_read),
        .mem_to_reg(mem_to_reg),
        .mem_write(mem_write),
        .ALU_src(ALU_src),
        .reg_write(reg_write),
        .ALU_control(ALU_control)   
    );    
        
    initial begin
        //test for derived ALU_control
        
        //branch
        //beq x1, x2, 16 -> opcode=7'b1100011, funct3=3'b000
        //Hex counterpart: 0x00208863
        opcode = 7'b1100011; funct3 = 3'b000;
        #10;
        
        //bne x1, x2, 16 -> opcode=7'b1100011, funct3=3'b001
        //Hex counterpart: 0x00209863
        opcode = 7'b1100011; funct3 = 3'b001;
        #10;
        
        //blt x1, x2, 16 -> opcode=7'b1100011, funct3=3'b100
        //Hex counterpart: 0x0020c863
        opcode = 7'b1100011; funct3 = 3'b100;
        #10;
        
        //bge x1, x2, 16 -> opcode=7'b1100011, funct3=3'b101
        //Hex counterpart: 0x0020d863
        opcode = 7'b1100011; funct3 = 3'b101;
        #10;
        
        //bltu x1, x2, 16 -> opcode=7'b1100011, funct3=3'b110
        //Hex counterpart: 0x0020e863
        opcode = 7'b1100011; funct3 = 3'b110;
        #10;
        
        //bgeu x1, x2, 16 -> opcode=7'b1100011, funct3=3'b111
        //Hex counterpart: 0x0020f863
        opcode = 7'b1100011; funct3 = 3'b111;
        #10;
        
        
        
        //loads
        //lb x1, 4(x2) -> opcode=7'b0000011, funct3=3'b000
        //Hex counterpart: 0x00410083
        opcode = 7'b0000011; funct3 = 3'b000;
        #10;
        
        //lh x1, 4(x2) -> opcode=7'b0000011, funct3=3'b001
        //Hex counterpart: 0x00411083
        opcode = 7'b0000011; funct3 = 3'b001;
        #10;
        
        //lw x1, 4(x2) -> opcode=7'b0000011, funct3=3'b010
        //Hex counterpart: 0x00412083
        opcode = 7'b0000011; funct3 = 3'b010;
        #10;
        
        //lbu x1, 4(x2) -> opcode=7'b0000011, funct3=3'b100
        //Hex counterpart: 0x00414083
        opcode = 7'b0000011; funct3 = 3'b100;
        #10;
        
        //lhu x1, 4(x2) -> opcode=7'b0000011, funct3=3'b101
        //Hex counterpart: 0x00415083
        opcode = 7'b0000011; funct3 = 3'b101;
        #10;
        
        
        
        //stores
        //sb x1, 4(x2) -> opcode=7'b0100011, funct3=3'b000
        //Hex counterpart: 0x00110223
        opcode = 7'b0100011; funct3 = 3'b000;
        #10;
        
        //sh x1, 4(x2) -> opcode=7'b0100011, funct3=3'b001
        //Hex counterpart: 0x00111223
        opcode = 7'b0100011; funct3 = 3'b001;
        #10;
        
        //sw x1, 4(x2) -> opcode=7'b0100011, funct3=3'b010
        //Hex counterpart: 0x00112223
        opcode = 7'b0100011; funct3 = 3'b010;
        #10;
        
        
        
        //immediates
        //addi x1, x2, 5 -> opcode=7'b0010011, funct3=3'b000
        //Hex counterpart: 0x00510093
        opcode = 7'b0010011; funct3 = 3'b000;
        #10;
        
        //slti x1, x2, 5 -> opcode=7'b0010011, funct3=3'b010
        //Hex counterpart: 0x00512093
        opcode = 7'b0010011; funct3 = 3'b010;
        #10;
        
        //sltiu x1, x2, 5 -> opcode=7'b0010011, funct3=3'b011
        //Hex counterpart: 0x00513093
        opcode = 7'b0010011; funct3 = 3'b011;
        #10;
        
        //xori x1, x2, 5 -> opcode=7'b0010011, funct3=3'b100
        //Hex counterpart: 0x00514093
        opcode = 7'b0010011; funct3 = 3'b100;
        #10;
        
        //ori x1, x2, 5 -> opcode=7'b0010011, funct3=3'b110
        //Hex counterpart: 0x00516093
        opcode = 7'b0010011; funct3 = 3'b110;
        #10;
        
        //andi x1, x2, 5 -> opcode=7'b0010011, funct3=3'b111
        //Hex counterpart: 0x00517093
        opcode = 7'b0010011; funct3 = 3'b111;
        #10;
        
        //slli x1, x2, 5 -> opcode=7'b0010011, funct3=3'b001, funct7=7'b0000000
        //Hex counterpart: 0x00511093
        opcode = 7'b0010011; funct3 = 3'b001; funct7 = 7'b0000000;
        #10;
        
        //srli x1, x2, 5 -> opcode=7'b0010011, funct3=3'b101, funct7=7'b0000000
        //Hex counterpart: 0x00515093
        opcode = 7'b0010011; funct3 = 3'b101; funct7 = 7'b0000000;
        #10;
        
        //srai x1, x2, 5 -> opcode=7'b0010011, funct3=3'b101, funct7=7'b0100000
        //Hex counterpart: 0x40515093
        opcode = 7'b0010011; funct3 = 3'b101; funct7 = 7'b0100000;
        #10;
        
        //arithmetics
        //add x1, x2, x3 -> opcode=7'b0110011, funct3=3'b000, funct7=7'b0000000
        //Hex counterpart: 0x003100b3
        opcode = 7'b0110011; funct3 = 3'b000; funct7 = 7'b0000000;
        #10;
        
        //sub x1, x2, x3 -> opcode=7'b0110011, funct3=3'b000, funct7=7'b0100000
        //Hex counterpart: 0x403100b3
        opcode = 7'b0110011; funct3 = 3'b000; funct7 = 7'b0100000;
        #10;
        
        //sll x1, x2, x3 -> opcode=7'b0110011, funct3=3'b001, funct7=7'b0000000
        //Hex counterpart: 0x003110b3
        opcode = 7'b0110011; funct3 = 3'b001; funct7 = 7'b0000000;
        #10;
        
        //slt x1, x2, x3 -> opcode=7'b0110011, funct3=3'b010, funct7=7'b0000000
        //Hex counterpart: 0x003120b3
        opcode = 7'b0110011; funct3 = 3'b010; funct7 = 7'b0000000;
        #10;
        
        //sltu x1, x2, x3 -> opcode=7'b0110011, funct3=3'b011, funct7=7'b0000000
        //Hex counterpart: 0x003130b3
        opcode = 7'b0110011; funct3 = 3'b011; funct7 = 7'b0000000;
        #10;
        
        //xor x1, x2, x3 -> opcode=7'b0110011, funct3=3'b100, funct7=7'b0000000
        //Hex counterpart: 0x003140b3
        opcode = 7'b0110011; funct3 = 3'b100; funct7 = 7'b0000000;
        #10;
        
        //srl x1, x2, x3 -> opcode=7'b0110011, funct3=3'b101, funct7=7'b0000000
        //Hex counterpart: 0x003150b3
        opcode = 7'b0110011; funct3 = 3'b101; funct7 = 7'b0000000;
        #10;
        
        //sra x1, x2, x3 -> opcode=7'b0110011, funct3=3'b101, funct7=7'b0100000
        //Hex counterpart: 0x403150b3
        opcode = 7'b0110011; funct3 = 3'b101; funct7 = 7'b0100000;
        #10;
        
        //or x1, x2, x3 -> opcode=7'b0110011, funct3=3'b110, funct7=7'b0000000
        //Hex counterpart: 0x003160b3
        opcode = 7'b0110011; funct3 = 3'b110; funct7 = 7'b0000000;
        #10;
        
        //and x1, x2, x3 -> opcode=7'b0110011, funct3=3'b111, funct7=7'b0000000
        //Hex counterpart: 0x003170b3
        opcode = 7'b0110011; funct3 = 3'b111; funct7 = 7'b0000000;
        #10;
        $finish;
        end
endmodule
