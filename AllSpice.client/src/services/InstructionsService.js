import { AppState } from "../AppState";
import { api } from "./AxiosService";

class InstructionsService {

  async getInstructions(id) {
    const res = await api.get(`/api/recipes/${id}/instructions`)
    AppState.instructions = res.data
  }

  async addInstruction(newInstruction, id) {
    const res = await api.post(`/api/recipes/${id}/instructions`, newInstruction)
    AppState.instructions.push(res.data)
  }

  setActiveInstruction(instruction) {
    AppState.activeInstruction = instruction
    console.log(AppState.activeIngredient);
  }
  async editInstruction(instructionData) {
    const res = await api.put(`/api/instructions/${instructionData.id}`, instructionData)
    AppState.instructions = res.data
  }

  async deleteInstruction(id) {
    await api.delete(`api/instructions/${id}`)
    AppState.instructions = AppState.instructions.filter(i => i.id != id)
  }
}

export const instructionsService = new InstructionsService();