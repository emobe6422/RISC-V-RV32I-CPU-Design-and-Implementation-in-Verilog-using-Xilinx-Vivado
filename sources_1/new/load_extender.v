`timescale 1ns / 1ps

module load_extender(
    input wire [31:0] raw_data,
    input wire [2:0] funct3,
    output reg [31:0] extended_data
);
    always @(*) begin
        case (funct3)
            3'b000: extended_data = {{24{raw_data[7]}},  raw_data[7:0]};   // lb
            3'b001: extended_data = {{16{raw_data[15]}}, raw_data[15:0]};  // lh
            3'b010: extended_data = raw_data;                              // lw
            3'b100: extended_data = {24'b0, raw_data[7:0]};                // lbu
            3'b101: extended_data = {16'b0, raw_data[15:0]};               // lhu
            default: extended_data = raw_data;
        endcase
    end
endmodule