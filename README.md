# SmartChargingAssignment

## Scenario:

Version: 21 
Your goal is to develop an API that exposes a simplified smart charging domain. 

**Domain model:**

![image](https://github.com/user-attachments/assets/30e7b2a1-8596-4504-a46f-3eff36888998)

* Group – has a unique **Identifier** (cannot be changed), **Name** (can be changed), **Capacity in Amps**
(integer, value greater than zero, can be changed). A Group can contain multiple charge stations.

* Charge station – has a unique **Identifier** (cannot be changed), **Name** (can be changed), and **Connectors**
(at least one, but not more than 5).

* Connector – has integer **Identifier** unique within the context of a charge station with (possible range
of values from 1 to 5), **Max current in Amps** (integer, value greater than zero, can be changed).

**Functional requirements:**
1. Group/Charge Station/Connector can be created, updated, and removed.
2. If a Group is removed, all Charge Stations in the Group are removed as well.
3. Only one Charge Station can be added/removed to a Group in one call.
4. The Charge Station can be only in one Group at the same time.
The Charge Station cannot exist in the domain without a Group.
5. A Connector cannot exist in the domain without a Charge Station.
6. The **Max current in Amps** of an existing Connector can be changed (updated).
7. The **Capacity in Amps** of a Group should always be greater than or equal to the sum of the
**Max current in Amps** of all Connectors indirectly belonging to the Group.
8. All operations/requests not meeting the above requirement should be rejected.
