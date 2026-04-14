`timescale 1ns / 1ps

//Only R and I formats rn

 module decoder(
//start with R and I type. Later S and U
    input wire [31:0] instruction,
    output wire [6:0] opcode,
    //register destination
    output wire [4:0] rd,
    //extra parameters for ALU operation (AND, OR, add, sub, etc.)
    output wire [2:0] funct3,
    output wire [6:0] funct7,
    //register sources #1 and #2
    output wire [4:0] rs1,
    output wire [4:0] rs2,
    
 
    //We don't need an immediate for R type since no immediates
    output wire [31:0] Iimm //sign extended Immediate opcode
    );

    //Get opcode
    assign opcode = instruction[6:0];
    //Get register destination
    assign rd  = instruction[11:7];
    //Get funct3 and funct7
    assign funct3 = instruction[14:12];
    assign funct7 = instruction[31:25]; 
    //Get Source Resigters.
    assign rs1 = instruction[19:15];
    assign rs2 = instruction[24:20];
    

    
    
    
    
    

    /*
    * RETURN TO IMMEDIATES
    * SHOULD HAVE A MUX TO DECIDE WHICH IMM TO USE NEXT
    * B type instructions next
    * I is not shifted but B (branch) and J (jump) are
    */
    //assign immediates (signed sign extension)
    //they also need that move left 1 bit thing
    assign Iimm = {{21{instruction[31]}},instruction[30:20]};
endmodule
