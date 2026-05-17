`timescale 1ns / 1ps

module rv32(
    input wire clk
    );
    
    /*           decoder          */
    wire [31:0] instruction;
    wire [6:0] opcode;
    wire [4:0] write_register, rs1, rs2;
    wire [2:0] funct3;
    wire [6:0] funct7;
    
    /*      immediate generator   */
    //wire [31:0] instruction;
    wire [31:0] I_immediate;
    wire [31:0] S_immediate;
    wire [31:0] B_immediate;
    wire [31:0] U_immediate;
    wire [31:0] J_immediate;
    
    /*         control            */
    //wire [6:0] opcode;
    //wire [2:0] funct3;
    //input wire [6:0] funct7;
     wire  branch, mem_read, mem_to_reg, 
           mem_write, ALU_src;
     wire reg_write;
     wire [3:0] ALU_control;
     //wire [3:0] write_mask;

    /*       register file        */
    wire [31:0] write_data;
    //wire [4:0] rs1;
    //wire [4:0] rs2;
    //wire [4:0] write_register;
    //wire reg_write;
    wire [31:0] read_data_1;
    wire [31:0] read_data_2;
    
    /*             ALU            */
    //wire [31:0] read_data_1;
    wire [31:0] data_2_mux;
    //wire [3:0] ALU_control;
    //wire [2:0] funct3;
    wire branch_flag;
    wire [31:0] sum;
    
    /*        data memory        */
    //wire clk;
    //wire mem_read;
    //wire mem_write;
    wire [31:0] address;
    //wire [31:0] ram_write_data;   REMOVED 
    wire [3:0] write_mask;
    wire [31:0] read_data;
    
    /*program counter and register*/
    //wire clk;    
    wire [31:0] PC;
    wire [31:0] next_PC;
    //wire branch_mux_signal;
    //wire [31:0] B_immediate;
    //wire [31:0] PC;
    //wire [31:0] next_PC;
    
    //decoder module instantiation
    decoder decoder(
        .instruction(instruction),
        .opcode(opcode),
        .write_register(write_register),
        .rs1(rs1),
        .rs2(rs2),
        .funct3(funct3),
        .funct7(funct7)        
    );
    
    //immediate generator instantiation
    immediate_generator immediate_generator(
        .instruction(instruction),
        .I_immediate(I_immediate),
        .S_immediate(S_immediate),
        .B_immediate(B_immediate),
        .U_immediate(U_immediate),
        .J_immediate(J_immediate)      
    );
    
    //control instantiation
    control control(
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
    
    //register file instantiation
    register_file register_file(
        .clk(clk),
        .write_data(write_data),
        .rs1(rs1),
        .rs2(rs2),
        .write_register(write_register),
        .reg_write(reg_write),
        .read_data_1(read_data_1),
        .read_data_2(read_data_2)
    );
    
    /*          immediate selection        */
    wire [31:0] immediate;
    assign immediate = (opcode == 7'b0100011) ? S_immediate :
                       (opcode == 7'b1100011) ? B_immediate :
                       (opcode == 7'b0110111) ? U_immediate :
                       (opcode == 7'b0010111) ? U_immediate :
                       (opcode == 7'b1101111) ? J_immediate :
                                                I_immediate;    
                                                                                  
    assign data_2_mux = (ALU_src == 0) ? read_data_2 : immediate;
    /*       return to instantiation       */
    
    //ALU instantiation
    ALU ALU(
        .read_data_1(read_data_1),
        .data_2_mux(data_2_mux),
        .ALU_control(ALU_control),
        .funct3(funct3),
        .branch_flag(branch_flag),
        .sum(sum)
    );
    
    /*      branch mux signal generation    */
     wire branch_mux_signal;
     assign branch_mux_signal = branch & branch_flag;
     /*       return to instantiation       */
     
     
     
     /*        drive address with sum       */
     assign address = sum;
     /*       return to instantiation       */
     
     
    
    //data memory instantiation
    data_memory data_memory (
        .clk(clk),
        .mem_read(mem_read),
        .mem_write(mem_write),
        .address(address),
        .ram_write_data(read_data_2), //read_data_2 not raw_write_data
        .write_mask(write_mask),
        .read_data(read_data)
    );
    
    /*   register write data multiplexor   */
    assign write_data = (mem_to_reg == 1) ? read_data : sum;
    /*       return to instantiation       */
    
    //program counter and register instantiation
    program_counter_register program_counter_register(
        .clk(clk),
        .next_PC(next_PC),
        .PC(PC)
    );

    program_counter program_counter(
        .branch_mux_signal(branch_mux_signal),
        .B_immediate(B_immediate),
        .PC(PC),
        .next_PC(next_PC)
    );
    
    instruction_memory instruction_memory(
        .address(PC),
        .instruction(instruction)
    );
    
    
    
    
    
    
    //load extender for sh, sb etc after sw and lw work
    
    
    //need some chunk after data memory to handle lh and lb!!!
    

endmodule
