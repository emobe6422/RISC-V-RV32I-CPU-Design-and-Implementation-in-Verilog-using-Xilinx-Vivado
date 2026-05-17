`timescale 1ns / 1ps

//combined control with ALU control

    module control(  
        input wire [6:0] opcode,
        input wire [2:0] funct3,
        input wire [6:0] funct7,
        output reg [3:0] write_mask, //only for stores
        output reg  branch, mem_read, mem_to_reg, 
        mem_write, ALU_src, reg_write,
        output reg [3:0] ALU_control
    );
    
    localparam lui_opcode = 7'b0110111;
    localparam auipc_opcode = 7'b0010111;
    localparam jal_opcode = 7'b1101111;
    localparam jalr_opcode = 7'b1100111;
    localparam branch_opcode = 7'b1100011;
    localparam load_opcode = 7'b0000011;
    localparam store_opcode = 7'b0100011;
    localparam immediate_opcode = 7'b0010011;
    localparam arithmetic_opcode = 7'b0110011; 
    
    
    //lb, lh, lhu, lbu, sb, sh, sw, slti, sltiu, auipc, jal, jalr, lui not implemented yet
    
    
    //maybe chnage the default ALU_control later to one not actually used
    
    always @(*) begin
        branch = 1'b0;
        mem_read = 1'b0;
        mem_to_reg = 1'b0;
        mem_write = 1'b0;
        ALU_src = 1'b0;
        reg_write = 1'b0; 
        ALU_control = 4'b0000;
        write_mask = 4'b0000;
            
        case (opcode)
            lui_opcode : begin
            
            end
            
            auipc_opcode : begin
            
            end
          
            jal_opcode : begin
            
            end
            
            jalr_opcode : begin
             
            end
            
            branch_opcode : begin //beq, bne, bge, bgeu, blt, bltu
                branch = 1'b1;
                ALU_control = 4'b1000;     
            end
            
            /*         first implement lw       */
            load_opcode : begin
                mem_read = 1'b1;
                mem_to_reg = 1'b1;  
                ALU_src = 1'b1;               
                reg_write = 1'b1;
                ALU_control = 4'b1010;     
            end
            
            /*         first implement sw       */
            store_opcode : begin
                mem_write = 1'b1;
                ALU_src = 1'b1;
                ALU_control = 4'b1001;
                case (funct3)
                    3'b000 : write_mask = 4'b0001; // sb - 1 byte
                    3'b001 : write_mask = 4'b0011; // sh - 2 bytes
                    3'b010 : write_mask = 4'b1111; // sw - 4 bytes
                    default: write_mask = 4'b0000;
                endcase
            end
            immediate_opcode : begin
                ALU_src = 1'b1;
                reg_write = 1'b1;
                case (funct3) 
                    3'b000 : ALU_control = 4'b0000; //addi uses add
                        // idk (slti)
                    3'b010 : begin
              
                    end
                        //idk (sltiu)
                    3'b011 : begin
                    
                    end
                    3'b100 : ALU_control = 4'b0001; //xori uses xor
                    3'b110 : ALU_control = 4'b0010; //ori uses or
                    3'b111 : ALU_control = 4'b0011;  //andi uses and  
                    3'b001 : ALU_control = 4'b0100; //slli uses sll
                    3'b101 : //srli and srai
                        case (funct7)    
                            7'b0000000 : ALU_control = 4'b0101; //srli uses srl
                            7'b0100000 : ALU_control = 4'b0110; //srai uses sra
                            default : ALU_control = 4'b0101; //default to 0000000
                        endcase
                endcase
            end
            arithmetic_opcode : begin
                reg_write = 1'b1;
                case (funct3)
                    3'b000 : //add/sub
                        case (funct7) 
                            7'b0100000 : ALU_control = 4'b0111; //sub
                            7'b0000000 : ALU_control = 4'b0000; //add
                            default : ALU_control = 4'b0000; //default to 0000000
                        endcase
                    3'b001 : ALU_control = 4'b0100; //sll
                    3'b010 : ALU_control = 4'b1011; //slt  
                    3'b011 : ALU_control = 4'b1100; //sltu
                    3'b100 : ALU_control = 4'b0001; //xor
                    3'b101 : begin 
                        //srl/sra
                        case (funct7) 
                            7'b0100000 : ALU_control = 4'b0110; //sra
                            7'b0000000 : ALU_control = 4'b0101; //srl
                            default : ALU_control = 4'b0101; //default to 0000000
                        endcase
                    end
                    3'b110 : ALU_control = 4'b0010; //or
                    3'b111 : ALU_control = 4'b0011; //and
                    default : ALU_control = 4'b0000;
                endcase
            end
            default : begin
                branch = 1'b0;
                mem_read = 1'b0;
                mem_to_reg = 1'b0;
                mem_write = 1'b0;
                ALU_src = 1'b0;
                reg_write = 1'b0;     
                ALU_control = 4'b0000;
                write_mask = 4'b0000;
            end      
        endcase
    end
endmodule