`timescale 1ns / 1ps

module SoC(
    input clk,
    input reset,
    input [31:0] instruction // CHANGE LATER
    );
   
    //decoder (with some overlap)
    wire [2:0] funct3;
    wire [6:0] funct7;
    wire [4:0] rs1;
    wire [4:0] rs2;
    wire [6:0] opcode;
    wire [4:0] rd;
    //Immediates
    wire [31:0] Iimm;
    //control_unit
    wire branch, memRead, memToReg, memWrite, ALUSrc, regWrite;
    wire [1:0] ALUOp;
    wire [3:0] ALUControl;
    //data
    wire [31:0] writeData;
    wire [31:0] readData1;
    wire [31:0] readData2;
    wire [31:0] sum;
    wire zeroFlag;
    wire [31:0] address;
    wire [31:0] addressData;



    //$readmemh
    
    
    
    
    
    decoder decoder_mod(
        .instruction(instruction),
        .funct3(funct3),
        .funct7(funct7),
        .rs1(rs1),
        .rs2(rs2),
        .opcode(opcode),
        .rd(rd),
        .Iimm(Iimm)
    );
    
    control_unit control_unit_mod(
        .opcode(opcode),
        .funct3(funct3),
        .funct7(funct7),
        .branch(branch),
        .memRead(memRead),
        .memToReg(memToReg),
        .memWrite(memWrite),
        .ALUSrc(ALUSrc),
        .regWrite(regWrite),
        .ALUOp(ALUOp),
        .ALUControl(ALUControl)
    );
    
    register_file register_file_mod(
        .clk(clk),
        .reset(reset),
        .regWrite(regWrite),
        .rs1(rs1),
        .rs2(rs2),
        .rd(rd),
        .writeData(writeData),
        .readData1(readData1),
        .readData2(readData2)
    );
    
    //THIS WILL NEED TO CHANGE ONCE WE DO OTHER IMMEDIATES
    wire [31:0] inputTwo;
    assign inputTwo = (ALUSrc == 1'b0) ? readData2 : Iimm;
    
    ALU ALU_mod(
        .readData1(readData1),
        .inputTwo(inputTwo),
        .ALUControl(ALUControl),
        .sum(sum),
        .zeroFlag(zeroFlag)
    );
    
    data_memory data_memory_mod(
        .clk(clk),
        .memWrite(memWrite),
        .memRead(memRead),
        .rs2(readData2),
        .address(sum),
        .addressData(addressData)
    );
    
    assign writeData = (memToReg == 1'b0) ? sum : addressData;
    
    
    //needed for branching (beq)
    //wire [31:0] shiftedIimm;
    //assign shiftedIimm = Iimm << 1;
    //needed for signal to branch (beq)
    
    //I dont support J or B yet but B will need to be shifted!
    
    //Not used yet since we only do R and I. therefore PC + 4 always
    wire branchingSignal;
    assign branchingSignal = branch & zeroFlag;

    
    
    
    //you can read values, just not assign them (wire and reg)
    always @(posedge clk) begin
    //reset somewhere here
    /**
    *
    *
    **/
    end
endmodule
