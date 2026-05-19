`timescale 1ns / 1ps
module tb_register_file();
    reg clk;
    reg [31:0] write_data;
    reg [4:0] rs1, rs2, write_register;
    reg reg_write;
    wire [31:0] read_data_1, read_data_2;
    integer i;
    integer pass_count, fail_count;
    
    // 50MHz
    initial clk = 0;
    always #10 clk = ~clk;
    
    register_file uut(
        .clk(clk),
        .write_data(write_data),
        .rs1(rs1),
        .rs2(rs2),
        .write_register(write_register),
        .reg_write(reg_write),
        .read_data_1(read_data_1),
        .read_data_2(read_data_2)
    );
    
    task check;
        input [127:0] test_name;
        input [31:0]  got, expected;
        begin
            if (got === expected) begin
                $display("PASS [%s]: got %h", test_name, got);
                pass_count = pass_count + 1;
            end else begin
                $display("FAIL [%s]: expected %h, got %h", test_name, expected, got);
                fail_count = fail_count + 1;
            end
        end
    endtask
    
    initial begin
        $dumpfile("tb_register_file.vcd");
        $dumpvars(0, tb_register_file);
        
        pass_count = 0;
        fail_count = 0;
        reg_write = 0;
        write_data = 0;
        write_register = 0;
        rs1 = 0;
        rs2 = 0;
        
        // ----------------------------------------
        // TEST 1: x0 hardwired to 0 (attempt write)
        // ----------------------------------------
        $display("\n--- TEST 1: x0 hardwired zero ---");
        reg_write = 1; write_register = 5'd0; write_data = 32'hFFFFFFFF;
        @(posedge clk); #1;
        reg_write = 0;
        rs1 = 5'd0; rs2 = 5'd0;
        @(posedge clk); #1;
        check("x0_rd1", read_data_1, 32'h00000000);
        check("x0_rd2", read_data_2, 32'h00000000);
        
        // ----------------------------------------
        // TEST 2: Basic write then read
        // ----------------------------------------
        $display("\n--- TEST 2: Basic write/read ---");
        reg_write = 1; write_register = 5'd1; write_data = 32'hDEADBEEF;
        @(posedge clk); #1;
        reg_write = 0;
        rs1 = 5'd1;
        @(posedge clk); #1;
        check("basic_write", read_data_1, 32'hDEADBEEF);
        
        // ----------------------------------------
        // TEST 3: reg_write=0 does not overwrite
        // ----------------------------------------
        $display("\n--- TEST 3: reg_write=0 no write ---");
        reg_write = 0; write_register = 5'd1; write_data = 32'hCAFEBABE;
        @(posedge clk); #1;
        rs1 = 5'd1;
        @(posedge clk); #1;
        check("no_write", read_data_1, 32'hDEADBEEF); // should still be old value
        
        // ----------------------------------------
        // TEST 4: Dual port simultaneous read
        // ----------------------------------------
        $display("\n--- TEST 4: Dual port read ---");
        reg_write = 1; write_register = 5'd1; write_data = 32'hAAAAAAAA;
        @(posedge clk); #1;
        reg_write = 1; write_register = 5'd2; write_data = 32'hBBBBBBBB;
        @(posedge clk); #1;
        reg_write = 0;
        rs1 = 5'd1; rs2 = 5'd2;
        @(posedge clk); #1;
        check("dual_rs1", read_data_1, 32'hAAAAAAAA);
        check("dual_rs2", read_data_2, 32'hBBBBBBBB);
        
        // ----------------------------------------
        // TEST 5: rs1 == rs2 same register both ports
        // ----------------------------------------
        $display("\n--- TEST 5: rs1 == rs2 ---");
        rs1 = 5'd1; rs2 = 5'd1;
        @(posedge clk); #1;
        check("same_port1", read_data_1, 32'hAAAAAAAA);
        check("same_port2", read_data_2, 32'hAAAAAAAA);
        
        // ----------------------------------------
        // TEST 6: All 32 registers write + readback
        // ----------------------------------------
        $display("\n--- TEST 6: All 32 registers ---");
        for (i = 1; i < 32; i = i + 1) begin
            reg_write = 1;
            write_register = i[4:0];
            write_data = i * 32'h11111111;
            @(posedge clk); #1;
        end
        reg_write = 0;
        for (i = 1; i < 32; i = i + 1) begin
            rs1 = i[4:0];
            @(posedge clk); #1;
            check("all_regs", read_data_1, i * 32'h11111111);
        end
        
        // ----------------------------------------
        // TEST 7: Boundary registers x1 and x31
        // ----------------------------------------
        $display("\n--- TEST 7: Boundary x1 and x31 ---");
        reg_write = 1; write_register = 5'd1;  write_data = 32'h00000001;
        @(posedge clk); #1;
        reg_write = 1; write_register = 5'd31; write_data = 32'hFFFFFFFF;
        @(posedge clk); #1;
        reg_write = 0;
        rs1 = 5'd1; rs2 = 5'd31;
        @(posedge clk); #1;
        check("boundary_x1",  read_data_1, 32'h00000001);
        check("boundary_x31", read_data_2, 32'hFFFFFFFF);
        
        // ----------------------------------------
        // SUMMARY
        // ----------------------------------------
        $display("\n=============================");
        $display("RESULTS: %0d passed, %0d failed", pass_count, fail_count);
        $display("=============================\n");
        
        $finish;
    end
endmodule