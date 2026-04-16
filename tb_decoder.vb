`timescale 1ns / 1ps

module tb_decoder();
    //declare values
    reg [31:0] tb_instruction;
    //MUST be wire not reg
    wire [6:0] tb_opcode;
    wire [2:0] tb_funct3;
    wire [6:0] tb_funct7;
    wire [4:0] tb_rs1, tb_rs2, tb_rd;
    wire [31:0] tb_Iimm, tb_Simm, tb_Bimm;
    
    //create a UUT for decoder
    decoder uut (
        .instruction(tb_instruction),
        .opcode(tb_opcode),
        .funct3(tb_funct3),
        .funct7(tb_funct7),
        .rs1(tb_rs1),
        .rs2(tb_rs2),
        .rd(tb_rd),
        .Iimm(tb_Iimm),
        .Simm(tb_Simm),
        .Bimm(tb_Bimm)
    );

    //stimulus process
    initial begin
    $display("decoder testbench");
    //ADD
    tb_instruction = 32'b00000000001100010000000010110011;
    #10;
    $display("Instruction: add x1, x2, x3");
    $display("Opcode: %b | rd: %d | rs1: %d | rs2: %d | funct3: %b", tb_opcode, tb_rd, tb_rs1, tb_rs2, tb_funct3);
    
    //SUB
    tb_instruction = 32'b01000000011000101000001000110011;
    #10;
    $display("Instruction: sub x4, x5, x6");
    $display("Opcode: %b | rd: %d | rs1: %d | rs2: %d | funct7: %b", tb_opcode, tb_rd, tb_rs1, tb_rs2, tb_funct7);
   
    //AND
    tb_instruction = 32'b00000000100101000111001110110011;
    #10;
    $display("Instruction: and x7, x8, x9");
    $display("Opcode: %b | rd: %d | rs1: %d | rs2: %d | funct3: %b", tb_opcode, tb_rd, tb_rs1, tb_rs2, tb_funct3);
    
    //XOR
     tb_instruction = 32'b00000000110001011100010100110011;
    #10;
    $display("Instruction: xor x10, x11, x12");
    $display("Opcode: %b | rd: %d | rs1: %d | rs2: %d | funct3: %b", tb_opcode, tb_rd, tb_rs1, tb_rs2, tb_funct3);
    
    //OR
    tb_instruction = 32'b00000000010100011110000010110011;
    #10;
    $display("Instruction: or x1, x3, x5");
    $display("Opcode: %b | rd: %d | rs1: %d | rs2: %d | funct3: %b", tb_opcode, tb_rd, tb_rs1, tb_rs2, tb_funct3);
    
    //LOAD WORD
    tb_instruction = 32'b00000000100001110010011010000011;
    #10;
    //$signed is added to interpret the # as a signed # since Iimm can hold + and - numbers
    $display("Instruction: lw x13, 8(x14)");
    $display("Opcode: %b | rd: %d | rs1: %d | I-Imm: %d", tb_opcode, tb_rd, tb_rs1, $signed(tb_Iimm));
    
    //STORE WORD
    tb_instruction = 32'b00000000111110000010011000100011;
    #10;
    $display("Instruction: sw x15, 12(x16)");
    $display("Opcode: %b | rs1: %d | rs2: %d | S-Imm: %d", tb_opcode, tb_rs1, tb_rs2, $signed(tb_Simm));
    
    //BRANCH IF EQUAL
    tb_instruction = 32'b00000001001010001000100001100011;
    #10;
    $display("Instruction: beq x17, x18, 16");
    $display("Opcode: %b | rs1: %d | rs2: %d | B-Imm: %d", tb_opcode, tb_rs1, tb_rs2, $signed(tb_Bimm)); 
    //putting #10 indicates to wait for 10 ns. Use this to let the instruction settle
    $display("verification complete");
    $stop; // Pause simulation
    end
endmodule