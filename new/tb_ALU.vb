`timescale 1ns / 1ps

module tb_ALU();
    reg [31:0] read_data_1;
    reg [31:0] data_2_mux;
    reg [3:0] ALU_control;
    reg [2:0] funct3;
    wire branch_flag;
    wire [31:0] sum;
    
    ALU uut(
        .read_data_1(read_data_1),
        .data_2_mux(data_2_mux),
        .ALU_control(ALU_control),
        .funct3(funct3),
        .branch_flag(branch_flag),
        .sum(sum)
    );

    initial begin       
        // add x1, x2, x3 | expected sum=8, branch=0
        read_data_1 = 32'd5;       data_2_mux = 32'd3;          ALU_control = 4'b0000; funct3 = 3'bx;
        #10;
        
        // xor x1, x2, x3 | expected sum=6, branch=0
        read_data_1 = 32'd5;       data_2_mux = 32'd3;          ALU_control = 4'b0001; funct3 = 3'bx;
        #10;
        
        // or x1, x2, x3 | expected sum=7, branch=0
        read_data_1 = 32'd5;       data_2_mux = 32'd3;          ALU_control = 4'b0010; funct3 = 3'bx;
        #10;
        
        // and x1, x2, x3 | expected sum=1, branch=0
        read_data_1 = 32'd5;       data_2_mux = 32'd3;          ALU_control = 4'b0011; funct3 = 3'bx;
        #10;
        
        // sll x1, x2, x3 | expected sum=16, branch=0
        read_data_1 = 32'd1;       data_2_mux = 32'd4;          ALU_control = 4'b0100; funct3 = 3'bx;
        #10;
        
        // srl x1, x2, x3 | expected sum=4, branch=0
        read_data_1 = 32'd16;      data_2_mux = 32'd2;          ALU_control = 4'b0101; funct3 = 3'bx;
        #10;
        
        // sra x1, x2, x3 | expected sum=FFFFFFFC, branch=0
        read_data_1 = 32'hFFFFFFF0; data_2_mux = 32'd2;         ALU_control = 4'b0110; funct3 = 3'bx;
        #10;
        
        // sub x1, x2, x3 | expected sum=7, branch=0
        read_data_1 = 32'd10;      data_2_mux = 32'd3;          ALU_control = 4'b0111; funct3 = 3'bx;
        #10;
        
        // beq x1, x2, 0 | expected sum=0, branch=1
        read_data_1 = 32'd5;       data_2_mux = 32'd5;          ALU_control = 4'b1000; funct3 = 3'b000;
        #10;
        
        // bne x1, x2, 0 | expected sum=0, branch=1
        read_data_1 = 32'd5;       data_2_mux = 32'd9;          ALU_control = 4'b1000; funct3 = 3'b001;
        #10;
        
        // blt x1, x2, 0 | expected sum=0, branch=1
        read_data_1 = 32'hFFFFFFFE; data_2_mux = 32'd1;         ALU_control = 4'b1000; funct3 = 3'b100;
        #10;
        
        // bge x1, x2, 0 | expected sum=0, branch=1
        read_data_1 = 32'd5;       data_2_mux = 32'd3;          ALU_control = 4'b1000; funct3 = 3'b101;
        #10;
        
        // bltu x1, x2, 0 | expected sum=0, branch=1
        read_data_1 = 32'd2;       data_2_mux = 32'd9;          ALU_control = 4'b1000; funct3 = 3'b110;
        #10;
        
        // bgeu x1, x2, 0 | expected sum=0, branch=1
        read_data_1 = 32'd9;       data_2_mux = 32'd3;          ALU_control = 4'b1000; funct3 = 3'b111;
        #10;
        
        // sw x2, 0(x1) | expected sum=100, branch=0
        read_data_1 = 32'd100;     data_2_mux = 32'd0;          ALU_control = 4'b1001; funct3 = 3'bx;
        #10;
        
        // lw x1, 0(x2) | expected sum=100, branch=0
        read_data_1 = 32'd100;     data_2_mux = 32'd0;          ALU_control = 4'b1010; funct3 = 3'bx;
        #10;
        
        // slt x1, x2, x3 | expected sum=1, branch=0
        read_data_1 = 32'hFFFFFFFE; data_2_mux = 32'd1;         ALU_control = 4'b1011; funct3 = 3'bx;
        #10;
        
        // sltu x1, x2, x3 | expected sum=1, branch=0
        read_data_1 = 32'd2;       data_2_mux = 32'd9;          ALU_control = 4'b1100; funct3 = 3'bx;
        #10;
        
        $finish;
    end
endmodule