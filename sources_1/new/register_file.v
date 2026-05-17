`timescale 1ns / 1ps
module register_file(
    input clk,
    input wire [31:0] write_data,
    input wire [4:0] rs1, rs2, write_register,
    input wire reg_write,
    output reg [31:0] read_data_1, read_data_2
    );
    
    reg [31:0] registers [0:31];
    
    integer i;
    initial begin
        for(i = 0; i < 32; i = i + 1)
            registers[i] = 32'b0;
    end

    always @(*) begin
        read_data_1 = (rs1 == 5'b00000) ? 32'b0 : registers[rs1];
        read_data_2 = (rs2 == 5'b00000) ? 32'b0 : registers[rs2];
    end

    always @(posedge clk) begin
        if (reg_write) begin
            registers[write_register] <= write_data;
        end
    end

endmodule